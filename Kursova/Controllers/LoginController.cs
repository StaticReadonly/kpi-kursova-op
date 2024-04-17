using Microsoft.AspNetCore.Mvc;

namespace Kursova.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
