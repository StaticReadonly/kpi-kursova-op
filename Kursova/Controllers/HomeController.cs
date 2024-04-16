using Microsoft.AspNetCore.Mvc;

namespace Kursova.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
