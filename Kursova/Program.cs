using FluentValidation;
using Kursova.ProgramConfigs;
using Microsoft.EntityFrameworkCore;
using Models.Options;
using System.Reflection;

namespace Kursova
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ValidatorOptions.Global.LanguageManager.Enabled = false;

            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var config = builder.Configuration;

            config.AddUserSecrets(Assembly.GetExecutingAssembly());
            config.AddJsonFile("Configuration/config.json");

            services.Configure<PaginationOptions>(config.GetSection("Pagination"));

            services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy("Cookies", cfg =>
                {
                    cfg.RequireAuthenticatedUser();
                    cfg.RequireClaim("Id");
                });
            });
            services.AddAuthentication()
                .AddCookie("Cookies", cfg =>
                {
                    cfg.LoginPath = "/login";

                    var opts = cfg.Cookie;
                    opts.Path = "/";
                    opts.HttpOnly = true;
                    opts.Name = "auth";
                    opts.SecurePolicy = CookieSecurePolicy.Always;
                    opts.SameSite = SameSiteMode.Strict;
                });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMvc();

            services.AddDbContext<DatabaseContext.DatabaseContext>(cfg =>
            {
                cfg.UseNpgsql(config["Database:ConnectionString"]);
            });

            //TenderStateInfo
            ProgramTenderStateInfo.AddTenderStateInfo(services);
            //PasswordService
            ProgramPasswordService.AddPasswordService(services);
            //LogInService
            ProgramLogInService.AddLogInService(services);
            //repositories
            ProgramRepositories.AddRepositories(services);
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

            app.UseStatusCodePagesWithRedirects("/error");
            app.UseExceptionHandler("/error");

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
