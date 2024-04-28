using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DbModels;
using Models.ViewModels;
using Services.Abstractions;

namespace Kursova.Controllers
{
    [Route("user")]
    [Authorize("Cookies")]
    public class UserController : Controller
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ILogInService _logInService;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;

        public UserController(
            IUsersRepository usersRepository,
            ILogInService logInService,
            ILogger<UserController> logger,
            IMapper mapper)
        {
            _usersRepository = usersRepository;
            _logInService = logInService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string guid = User.Claims.First(c => c.Type == "Id").Value;

            UserModel? data = await _usersRepository.GetUserDataById(guid);

            if (data == null)
            {
                _logger.LogError($"User with invalid id [{guid}] detected");
                await _logInService.LogOut(HttpContext);
                return Redirect("/");
            }

            UserViewModel model = _mapper.Map<UserViewModel>(data);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _logInService.LogOut(HttpContext);
            return Redirect("/");
        }
    }
}
