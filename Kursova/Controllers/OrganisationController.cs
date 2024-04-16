using Microsoft.AspNetCore.Mvc;

namespace Kursova.Controllers
{
    [Route("organisation")]
    public class OrganisationController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
