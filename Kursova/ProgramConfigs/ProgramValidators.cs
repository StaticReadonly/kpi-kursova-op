using FluentValidation;
using Models.ControllerModels;
using Models.ModeValidators.ControllerModels;

namespace Kursova.ProgramConfigs
{
    public static class ProgramValidators
    {
        public static void AddModelValidators(IServiceCollection services)
        {
            services.AddScoped<IValidator<UserLoginModel>, UserLoginModelValidator>();
            services.AddScoped<IValidator<UserRegistrationModel>, UserRegistrationModelValidator>();
        }
    }
}
