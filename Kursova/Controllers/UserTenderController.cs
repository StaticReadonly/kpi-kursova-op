using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.ControllerModels;
using Models.Exceptions;
using Services.Abstractions;

namespace Kursova.Controllers
{
    [Route("tenders")]
    [Authorize("Cookies")]
    public class UserTenderController : Controller
    {
        private readonly IValidator<UserTenderOffersModel> _userTenderOffersModelValidator;
        private readonly IValidator<OfferActionModel> _offerActionValidator;
        private readonly IValidator<NewTenderModel> _newTenderModelValidator;
        private readonly ILogger<UserTenderController> _logger;
        private readonly ITendersRepository _tendersRepository;
        private readonly IOffersRepository _offersRepository;

        public UserTenderController(
            IValidator<UserTenderOffersModel> validator,
            ILogger<UserTenderController> logger,
            ITendersRepository tendersRepository,
            IOffersRepository offersRepository,
            IValidator<OfferActionModel> offerActionValidator,
            IValidator<NewTenderModel> newTenderModelValidator)
        {
            _userTenderOffersModelValidator = validator;
            _logger = logger;
            _tendersRepository = tendersRepository;
            _offersRepository = offersRepository;
            _offerActionValidator = offerActionValidator;
            _newTenderModelValidator = newTenderModelValidator;
        }

        [HttpGet]
        public IActionResult Index([FromQuery] TenderSearchModel searchModel)
        {
            if (searchModel.Page < 1) return BadRequest();

            try
            {
                var guid = User.Claims.First(c => c.Type == "Id").Value;
                var res = _tendersRepository.GetUserTenders(searchModel, Guid.Parse(guid));

                return View(res);
            }
            catch (ArgumentException exc)
            {
                _logger.LogError(exc.Message);
                return BadRequest();
            }
        }

        [HttpGet("offers")]
        public IActionResult Offers([FromQuery] UserTenderOffersModel offersModel)
        {
            ValidationResult res = _userTenderOffersModelValidator.Validate(offersModel);

            if (!res.IsValid)
            {
                return BadRequest();
            }

            if (offersModel.Page < 1) return BadRequest();

            try
            {
                var guid = User.Claims.First(c => c.Type == "Id").Value;
                var resModel = _offersRepository.GetTenderOffers(offersModel, Guid.Parse(guid));

                return View(resModel);
            }catch(NoOffersException)
            {
                return View("NoOffers");
            }
            catch(ArgumentException exc)
            {
                _logger.LogError(exc.Message);
                return BadRequest();
            }
        }

        [HttpPost("offer/action")]
        public async Task<IActionResult> OfferAction([FromForm] OfferActionModel model)
        {
            var result = _offerActionValidator.Validate(model);

            if (!result.IsValid)
            {
                return BadRequest();
            }

            string guid = User.Claims.Where(c => c.Type == "Id").First().Value;

            try
            {
                await _offersRepository.OfferAction(Guid.Parse(guid), model);
                return Redirect("/");
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return BadRequest();
            }
        }

        [HttpGet("new")]
        public IActionResult NewTender()
        {
            return View();
        }

        [HttpPost("new")]
        public async Task<IActionResult> NewTender([FromForm] NewTenderModel model)
        {
            var result = _newTenderModelValidator.Validate(model);

            if (!result.IsValid)
            {
                result.AddToModelState(ModelState);

                return View(model);
            }

            Guid guid = Guid.Parse(User.Claims.First(c => c.Type == "Id").Value);
            await _tendersRepository.CreateNewTender(model, guid);

            return Redirect("/");
        }
    }
}
