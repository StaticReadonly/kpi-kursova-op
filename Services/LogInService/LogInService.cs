using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Models.DbModels;
using Models.Exceptions;
using Services.Abstractions;
using System.Security.Claims;

namespace Services.LogInService
{
    public class LogInService : ILogInService
    {
        private readonly IPasswordService _passwordService;
        private readonly IUsersRepository _usersRepository;

        public LogInService(
            IUsersRepository usersRepository, 
            IPasswordService passwordService)
        {
            _usersRepository = usersRepository;
            _passwordService = passwordService;
        }

        public async Task LogIn(HttpContext context, string email, string password)
        {
            UserModel? user = await _usersRepository.GetUserDataByEmail(email);
            
            if (user == null || !_passwordService.VerifyPassword(password, user.Password))
                throw new FormFieldException("Password", "Wrong login or password");

            Claim id = new Claim("Id", user.Id.ToString());
            ClaimsIdentity identity = new ClaimsIdentity(new Claim[] { id }, "Cookies");
            await context.SignInAsync(new ClaimsPrincipal(identity));
        }

        public async Task LogIn(HttpContext context, Guid id)
        {
            Claim claim = new Claim("Id", id.ToString());
            ClaimsIdentity identity = new ClaimsIdentity(new Claim[] { claim }, "Cookies");
            await context.SignInAsync(new ClaimsPrincipal(identity));
        }

        public async Task LogOut(HttpContext context)
        {
            await context.SignOutAsync("Cookies");
        }
    }
}
