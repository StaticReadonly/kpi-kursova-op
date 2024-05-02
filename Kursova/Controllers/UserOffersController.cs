using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.ControllerModels;
using Services.Abstractions;

namespace Kursova.Controllers
{
    [Route("offers")]
    [Authorize("Cookies")]
    public class UserOffersController : Controller
    {
        private readonly IOffersRepository _offersRepository;
        private readonly ILogger<UserOffersController> _logger;

        public UserOffersController(
            IOffersRepository offersRepository,
            ILogger<UserOffersController> logger)
        {
            _offersRepository = offersRepository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index([FromQuery] UserOffersModel offersModel)
        {
            if (offersModel.Page < 1) return BadRequest();

            try
            {
                var guid = User.Claims.Where(c => c.Type == "Id").First().Value;
                var res = _offersRepository.GetUserOffers(offersModel, Guid.Parse(guid));

                return View(res);
            }
            catch(ArgumentException exc)
            {
                _logger.LogError(exc.Message);
                return BadRequest();
            }
        }
    }
}
