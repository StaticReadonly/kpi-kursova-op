using Microsoft.AspNetCore.Mvc;

namespace Kursova.Controllers
{
    [Route("tenders")]
    public class OrganisationTendersController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
