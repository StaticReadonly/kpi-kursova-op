using Microsoft.AspNetCore.Mvc;
using Models.ControllerModels;
using Models.DbModels;
using Models.ViewModels;

namespace Kursova.Controllers
{
    [Route("/")]
    public class TenderSearchController : Controller
    {
        [HttpGet]
        public IActionResult Index([FromQuery] TenderSearchModel searchModel)
        {
            if (searchModel.Page < 1) return Redirect("/");

            int amount = 3;
            TenderSearchViewModel model = new TenderSearchViewModel();

            var tenders = Models;

            if (searchModel.Query != null)
            {
                model.Query = searchModel.Query;
                tenders = tenders.FindAll(m => m.Name.Contains(searchModel.Query));
            }

            model.CurrentPage = searchModel.Page;
            if (tenders.Count % amount != 0)
            {
                model.TotalPages = tenders.Count / amount + 1;
            }
            else
            {
                model.TotalPages = tenders.Count / amount;
            }

            if (searchModel.Page > model.TotalPages) return Redirect("/");

            model.Tenders = tenders.Skip((searchModel.Page - 1) * amount).Take(amount).ToList();

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
