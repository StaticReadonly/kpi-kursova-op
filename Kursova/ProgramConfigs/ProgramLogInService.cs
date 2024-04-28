using Services.Abstractions;
using Services.LogInService;

namespace Kursova.ProgramConfigs
{
    public static class ProgramLogInService
    {
        public static void AddLogInService(IServiceCollection services)
        {
            services.AddScoped<ILogInService, LogInService>();
        }
    }
}
