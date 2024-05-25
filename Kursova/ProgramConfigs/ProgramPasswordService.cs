using Services.Abstractions;
using Services.PasswordService;

namespace Kursova.ProgramConfigs
{
    public static class ProgramPasswordService
    {
        public static void AddPasswordService(IServiceCollection services)
        {
            services.AddScoped<IPasswordService, PasswordService>();
        }
    }
}
