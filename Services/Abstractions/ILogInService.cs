using Microsoft.AspNetCore.Http;

namespace Services.Abstractions
{
    public interface ILogInService
    {
        Task LogIn(HttpContext context, Guid id);
        Task LogIn(HttpContext context, string login, string password);
        Task LogOut(HttpContext context);
    }
}
