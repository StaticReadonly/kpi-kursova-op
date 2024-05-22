using Microsoft.AspNetCore.Mvc;

namespace Kursova.Controllers
{
    [Route("error")]
    public class ErrorHandler : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
