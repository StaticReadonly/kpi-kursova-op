using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;

namespace Kursova.Controllers
{
    [Route("user")]
    [Authorize("Cookies")]
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            UserViewModel m = new UserViewModel()
            {
                Name = "user",
                Surname = "user",
                Patronimyc = "user",
                Email = "user",
                Address = "user"
            };

            return View(m);
        }
    }
}
