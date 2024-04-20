using FluentValidation;
using Kursova.ProgramConfigs;
using Models.ControllerModels;
using Models.ModeValidators.ControllerModels;
using Models.Options;
using Services.Filters;

namespace Kursova
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var config = builder.Configuration;

            config.AddJsonFile("Configuration/config.json");

            services.Configure<PaginationOptions>(config.GetSection("Pagination"));

            services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy("Cookies", cfg =>
                {
                    cfg.RequireAuthenticatedUser();
                });
            });
            services.AddAuthentication()
                .AddCookie("Cookies", cfg =>
                {
                    cfg.LoginPath = "/login";
                });

            services.AddMvc();

            //filters
            ProgramFilters.AddFilters(services);
            //model validators
            ProgramValidators.AddModelValidators(services);

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
