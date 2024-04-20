using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models.ControllerModels;
using Models.Options;
using Models.ViewModels;

namespace Kursova.Controllers
{
    [Route("offers")]
    public class UserOffersController : Controller
    {
        private readonly PaginationOptions _paginationOptions;

        public UserOffersController(IOptions<PaginationOptions> paginationOptions)
        {
            _paginationOptions = paginationOptions.Value;
        }

        [HttpGet]
        public IActionResult Index([FromQuery] UserOffersModel offersModel)
        {
            if (offersModel.Page < 1) return BadRequest();

            UserOffersViewModel model = new UserOffersViewModel();

            var offers = Models;

            model.CurrentPage = offersModel.Page;
            if (offers.Count % _paginationOptions.ItemsPerPage != 0)
            {
                model.TotalPages = offers.Count / _paginationOptions.ItemsPerPage + 1;
            }
            else
            {
                model.TotalPages = offers.Count / _paginationOptions.ItemsPerPage;
            }

            if (offersModel.Page > model.TotalPages) return Redirect("/");

            model.Offers = offers.Skip((offersModel.Page - 1) * _paginationOptions.ItemsPerPage)
                .Take(_paginationOptions.ItemsPerPage)
                .ToList();

            return View(model);
        }

        private static List<UserOfferViewModel> Models = new List<UserOfferViewModel>()
        {
            new()
            {
                TenderName = "name",
                CreationDate = DateTime.Now,
                Description = "description",
                Price = 1.1m,
                State = "state"
            },
            new()
            {
                TenderName = "name",
                CreationDate = DateTime.Now,
                Description = "description",
                Price = 1.1m,
                State = "state"
            },
            new()
            {
                TenderName = "name",
                CreationDate = DateTime.Now,
                Description = "description",
                Price = 1.1m,
                State = "state"
            },
            new()
            {
                TenderName = "name",
                CreationDate = DateTime.Now,
                Description = "description",
                Price = 1.1m,
                State = "state"
            },
            new()
            {
                TenderName = "name",
                CreationDate = DateTime.Now,
                Description = "description",
                Price = 1.1m,
                State = "state"
            }
        };
    }
}
