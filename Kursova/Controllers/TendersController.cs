using Microsoft.AspNetCore.Mvc;

namespace Kursova.Controllers
{
    [Route("tenders")]
    public class TendersController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
