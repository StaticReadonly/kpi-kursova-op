using Microsoft.AspNetCore.Mvc;

namespace Kursova.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
