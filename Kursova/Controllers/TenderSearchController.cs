using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models.ControllerModels;
using Models.DbModels;
using Models.Options;
using Models.ViewModels;

namespace Kursova.Controllers
{
    [Route("/")]
    public class TenderSearchController : Controller
    {
        private readonly PaginationOptions _paginationOptions;

        public TenderSearchController(IOptions<PaginationOptions> paginationOptions)
        {
            _paginationOptions = paginationOptions.Value;
        }

        [HttpGet]
        public IActionResult Index([FromQuery] TenderSearchModel searchModel)
        {
            if (searchModel.Page < 1) return Redirect("/");

            TenderSearchViewModel model = new TenderSearchViewModel();

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

            if (searchModel.Page > model.TotalPages) return Redirect("/");

            model.Tenders = tenders.Skip((searchModel.Page - 1) * _paginationOptions.ItemsPerPage)
                .Take(_paginationOptions.ItemsPerPage)
                .ToList();

            return View(model);
        }

        private static List<TenderModel> Models = new List<TenderModel>() 
        {
            new()
            {
                Name = "Tender1",
                Cost = 1.1m,
                Description = "description1",
                CreationDate = DateTime.Now
            },
            new()
            {
                Name = "Tender2",
                Cost = 2.2m,
                Description = "description2",
                CreationDate = DateTime.Now
            },
            new()
            {
                Name = "Tender3",
                Cost = 3.3m,
                Description = "description3",
                CreationDate = DateTime.Now
            },
            new()
            {
                Name = "Tender4",
                Cost = 4.4m,
                Description = "description4",
                CreationDate = DateTime.Now
            },
            new()
            {
                Name = "Tender5",
                Cost = 5.5m,
                Description = "description5",
                CreationDate = DateTime.Now
            }
        };
    }
}
