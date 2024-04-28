using FluentValidation;
using Services.Filters;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Models.ControllerModels;
using Services.Abstractions;
using Models.Exceptions;

namespace Kursova.Controllers
{
    [Route("login")]
    [UnauthorizedUserFilter]
    public class LoginController : Controller
    {
        private readonly IValidator<UserLoginModel> _userLoginModelValidator;
        private readonly IValidator<UserRegistrationModel> _userRegistrationModelValidator;
        private readonly IUsersRepository _usersRepository;
        private readonly ILogInService _logInService;

        public LoginController(
            IValidator<UserLoginModel> userLoginControllerModelValidator,
            IValidator<UserRegistrationModel> userRegistrationModelValidator,
            ILogInService logInService,
            IUsersRepository usersRepository)
        {
            _userLoginModelValidator = userLoginControllerModelValidator;
            _userRegistrationModelValidator = userRegistrationModelValidator;
            _logInService = logInService;
            _usersRepository = usersRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] UserLoginModel loginModel)
        {
            ValidationResult res = _userLoginModelValidator.Validate(loginModel);
            
            if (!res.IsValid)
            {
                res.AddToModelState(ModelState);

                return View("Index");
            }

            try
            {
                await _logInService.LogIn(HttpContext, loginModel.Email!, loginModel.Password!);
                return Redirect("/user");
            }
            catch (FormFieldException exc)
            {
                ModelState.AddModelError(exc.Key, exc.Message);

                return View("Index");
            }
        }

        [HttpGet("new")]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost("new")]
        public async Task<IActionResult> Registration([FromForm] UserRegistrationModel registrationModel)
        {
            ValidationResult res = _userRegistrationModelValidator.Validate(registrationModel);

            if (!res.IsValid)
            {
                res.AddToModelState(ModelState);

                return View("Registration");
            }

            try
            {
                Guid id = await _usersRepository.CreateUser(registrationModel);
                await _logInService.LogIn(HttpContext, id);
                return Redirect("/user");
            }
            catch (FormFieldException ex)
            {
                ModelState.AddModelError(ex.Key, ex.Message);

                return View("Registration");
            }
        }
    }
}
