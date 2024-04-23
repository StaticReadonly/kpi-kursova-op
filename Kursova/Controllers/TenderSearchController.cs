﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Models.ControllerModels;
using Models.ViewModels.TenderSearch;
using Services.Abstractions;

namespace Kursova.Controllers
{
    [Route("/")]
    public class TenderSearchController : Controller
    {
        private readonly ILogger<TenderSearchController> _logger;
        private readonly IMapper _mapper;
        private readonly ITendersRepository _tendersRepository;

        public TenderSearchController(
            IMapper mapper,
            ITendersRepository tendersRepository,
            ILogger<TenderSearchController> logger
            )
        {
            _mapper = mapper;
            _tendersRepository = tendersRepository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index([FromQuery] TenderSearchModel searchModel)
        {
            try
            {
                var result = _tendersRepository.GetTenders(searchModel);

                return View(result);
            }
            catch(ArgumentException ex)
            {
                _logger.LogError($"Invalid page. Details: {ex.Message}");
                return Redirect("/");
            }
        }

        [HttpGet("tender/{id:guid}")]
        public IActionResult TenderInfo([FromRoute] Guid id)
        {
            try
            {
                var result = _tendersRepository.GetTenderInfo(id);

                return View(_mapper.Map<TenderInfoViewModel>(result));
            }
            catch(ArgumentException ex)
            {
                _logger.LogError(ex.Message);
                return Redirect("/");
            }
        }
    }
}
