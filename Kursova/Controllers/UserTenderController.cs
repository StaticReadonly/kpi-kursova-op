﻿using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models.ControllerModels;
using Models.DbModels;
using Models.Options;
using Models.ViewModels;
using Services.Abstractions;

namespace Kursova.Controllers
{
    [Route("tenders")]
    [Authorize("Cookies")]
    public class UserTenderController : Controller
    {
        private readonly IValidator<UserTenderOffersModel> _validator;
        private readonly ILogger<UserTenderController> _logger;
        private readonly ITendersRepository _tendersRepository;
        private readonly IOffersRepository _offersRepository;

        public UserTenderController(
            IValidator<UserTenderOffersModel> validator,
            ILogger<UserTenderController> logger,
            ITendersRepository tendersRepository,
            IOffersRepository offersRepository)
        {
            _validator = validator;
            _logger = logger;
            _tendersRepository = tendersRepository;
            _offersRepository = offersRepository;
        }

        [HttpGet]
        public IActionResult Index([FromQuery] TenderSearchModel searchModel)
        {
            if (searchModel.Page < 1) return BadRequest();

            try
            {
                var guid = User.Claims.Where(c => c.Type == "Id").First().Value;
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
            ValidationResult res = _validator.Validate(offersModel);

            if (!res.IsValid)
            {
                return BadRequest();
            }

            if (offersModel.Page < 1) return BadRequest();

            try
            {
                var guid = User.Claims.Where(c => c.Type == "Id").First().Value;
                var resModel = _offersRepository.GetTenderOffers(offersModel, Guid.Parse(guid));

                return View(resModel);
            }
            catch(ArgumentException exc)
            {
                _logger.LogError(exc.Message);
                return BadRequest();
            }
        }
    }
}
