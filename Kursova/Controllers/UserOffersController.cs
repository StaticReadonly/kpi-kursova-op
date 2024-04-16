using Microsoft.AspNetCore.Mvc;

namespace Kursova.Controllers
{
    [Route("offers")]
    public class UserOffersController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
