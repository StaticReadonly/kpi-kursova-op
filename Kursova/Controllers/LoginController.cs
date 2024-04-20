using FluentValidation;
using Services.Filters;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Models.ControllerModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Kursova.Controllers
{
    [Route("login")]
    [UnauthorizedUserFilter]
    public class LoginController : Controller
    {
        private readonly IValidator<UserLoginModel> _userLoginModelValidator;
        private readonly IValidator<UserRegistrationModel> _userRegistrationModelValidator;

        public LoginController(
            IValidator<UserLoginModel> userLoginControllerModelValidator,
            IValidator<UserRegistrationModel> userRegistrationModelValidator)
        {
            _userLoginModelValidator = userLoginControllerModelValidator;
            _userRegistrationModelValidator = userRegistrationModelValidator;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] UserLoginModel loginModel)
        {
            ValidationResult res = _userLoginModelValidator.Validate(loginModel);
            
            if (!res.IsValid)
            {
                res.AddToModelState(ModelState);

                return View("Index");
            }

            Claim email = new Claim("Email", loginModel.Email!);
            ClaimsIdentity identity = new ClaimsIdentity(new Claim[] { email }, "Cookies");
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("Cookies", principal);

            return Redirect("/");
        }

        [HttpGet("new")]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost("new")]
        public IActionResult Registration([FromForm] UserRegistrationModel registrationModel)
        {
            ValidationResult res = _userRegistrationModelValidator.Validate(registrationModel);

            if (!res.IsValid)
            {
                res.AddToModelState(ModelState);

                return View("Registration");
            }

            return Redirect("/");
        }
    }
}
