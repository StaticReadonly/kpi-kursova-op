using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.ControllerModels;
using Models.Exceptions;
using Services.Abstractions;

namespace Kursova.Controllers
{
    [Route("offers")]
    [Authorize("Cookies")]
    public class UserOffersController : Controller
    {
        private readonly IOffersRepository _offersRepository;
        private readonly ILogger<UserOffersController> _logger;
        private readonly IValidator<NewOfferModel> _newOfferValidator;

        public UserOffersController(
            IOffersRepository offersRepository,
            ILogger<UserOffersController> logger,
            IValidator<NewOfferModel> newOfferValidator)
        {
            _offersRepository = offersRepository;
            _logger = logger;
            _newOfferValidator = newOfferValidator;
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

        [HttpGet("{id:guid}/new")]
        public IActionResult NewOffer([FromRoute] Guid id)
        {
            NewOfferModel model = new NewOfferModel()
            {
                TenderId = id
            };

            return View(model);
        }

        [HttpPost("{id:guid}/new")]
        public async Task<IActionResult> NewOffer([FromForm] NewOfferModel model)
        {
            var guid = User.Claims.First(c => c.Type == "Id").Value;
            model.OwnerId = Guid.Parse(guid);

            var result = _newOfferValidator.Validate(model);

            if (!result.IsValid)
            {
                result.AddToModelState(ModelState);

                return View(model);
            }

            try
            {
                await _offersRepository.NewOffer(model);

                return Redirect("/offers");
            }catch(FormFieldException exc)
            {
                ModelState.AddModelError(exc.Key, exc.Message);

                return View(model);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return BadRequest();
            }
        }
    }
}
