using Microsoft.AspNetCore.Mvc;

namespace Kursova.Controllers
{
    [Route("/")]
    public class TenderSearchController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
