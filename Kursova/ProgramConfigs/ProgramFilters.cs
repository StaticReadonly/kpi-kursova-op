using Services.Filters;

namespace Kursova.ProgramConfigs
{
    public static class ProgramFilters
    {
        public static void AddFilters(IServiceCollection services)
        {
            services.AddScoped<UnauthorizedUserFilterAttribute>();
        }
    }
}
