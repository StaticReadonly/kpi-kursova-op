using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models.ControllerModels;
using Models.DbModels;
using Models.Options;
using Models.ViewModels;

namespace Kursova.Controllers
{
    [Route("tenders")]
    public class UserTenderController : Controller
    {
        private readonly IValidator<UserTenderOffersModel> _validator;
        private readonly PaginationOptions _paginationOptions;

        public UserTenderController(
            IOptions<PaginationOptions> paginationOptions, 
            IValidator<UserTenderOffersModel> validator
            )
        {
            _paginationOptions = paginationOptions.Value;
            _validator = validator;
        }

        [HttpGet]
        public IActionResult Index([FromQuery] TenderSearchModel searchModel)
        {
            if (searchModel.Page < 1) return Redirect("/tenders");

            UserTenderViewModel model = new UserTenderViewModel();

            var tenders = Models;

            if (searchModel.Query != null)
            {
                model.Query = searchModel.Query;
                tenders = tenders.FindAll(m => m.Name.Contains(searchModel.Query));
            }

            model.CurrentPage = searchModel.Page;
            if (tenders.Count % _paginationOptions.ItemsPerPage != 0)
            {
                model.TotalPages = tenders.Count / _paginationOptions.ItemsPerPage + 1;
            }
            else
            {
                model.TotalPages = tenders.Count / _paginationOptions.ItemsPerPage;
            }

            if (searchModel.Page > model.TotalPages) return Redirect("/tenders");

            model.Tenders = tenders.Skip((searchModel.Page - 1) * _paginationOptions.ItemsPerPage)
                .Take(_paginationOptions.ItemsPerPage)
                .ToList();

            return View(model);
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

            UserTenderOffersViewModel model = new UserTenderOffersViewModel();

            model.Id = offersModel.Id;

            var offers = Models2;

            model.CurrentPage = offersModel.Page;
            if (offers.Count % _paginationOptions.ItemsPerPage != 0)
            {
                model.TotalPages = offers.Count / _paginationOptions.ItemsPerPage + 1;
            }
            else
            {
                model.TotalPages = offers.Count / _paginationOptions.ItemsPerPage;
            }

            if (offersModel.Page > model.TotalPages) return Redirect("/tenders");

            model.Offers = offers.Skip((offersModel.Page - 1) * _paginationOptions.ItemsPerPage)
                .Take(_paginationOptions.ItemsPerPage)
                .ToList();

            return View(model);
        }

        private static List<TenderModel> Models = new List<TenderModel>()
        {
            new()
            {
                Id = Guid.Empty,
                Name = "Tender1",
                Cost = 1.1m,
                Description = "description1",
                CreationDate = DateTime.Now
            },
            new()
            {
                Id = Guid.Empty,
                Name = "Tender2",
                Cost = 2.2m,
                Description = "description2",
                CreationDate = DateTime.Now
            },
            new()
            {
                Id = Guid.Empty,
                Name = "Tender3",
                Cost = 3.3m,
                Description = "description3",
                CreationDate = DateTime.Now
            },
            new()
            {
                Id = Guid.Empty,
                Name = "Tender4",
                Cost = 4.4m,
                Description = "description4",
                CreationDate = DateTime.Now
            },
            new()
            {
                Id = Guid.Empty,
                Name = "Tender5",
                Cost = 5.5m,
                Description = "description5",
                CreationDate = DateTime.Now
            }
        };

        private static List<UserTenderOfferViewModel> Models2 = new List<UserTenderOfferViewModel>()
        {
            new()
            {
                Id = Guid.Empty,
                Offerer = new()
                {
                    Name = "user",
                    Surname = "user",
                    Patronimyc = "user"
                },
                CreationDate = DateTime.Now,
                Price = 1.1m,
                Description = "aboba"
            },
            new()
            {
                Id = Guid.Empty,
                Offerer = new()
                {
                    Name = "user",
                    Surname = "user",
                    Patronimyc = "user"
                },
                CreationDate = DateTime.Now,
                Price = 1.1m,
                Description = "aboba"
            },
            new()
            {
                Id = Guid.Empty,
                Offerer = new()
                {
                    Name = "user",
                    Surname = "user",
                    Patronimyc = "user"
                },
                CreationDate = DateTime.Now,
                Price = 1.1m,
                Description = "aboba"
            },
            new()
            {
                Id = Guid.Empty,
                Offerer = new()
                {
                    Name = "user",
                    Surname = "user",
                    Patronimyc = "user"
                },
                CreationDate = DateTime.Now,
                Price = 1.1m,
                Description = "aboba"
            },
            new()
            {
                Id = Guid.Empty,
                Offerer = new()
                {
                    Name = "user",
                    Surname = "user",
                    Patronimyc = "user"
                },
                CreationDate = DateTime.Now,
                Price = 1.1m,
                Description = "aboba"
            }
        };
    }
}
