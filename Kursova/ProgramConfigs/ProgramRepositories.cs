using Services.Abstractions;
using Services.Repositories;

namespace Kursova.ProgramConfigs
{
    public static class ProgramRepositories
    {
        public static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ITendersRepository, TendersRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IOffersRepository, OffersRepository>();
        }
    }
}
