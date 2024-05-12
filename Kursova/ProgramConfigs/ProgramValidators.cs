using FluentValidation;
using Models.ControllerModels;
using Models.ModelValidators.ControllerModels;
using Models.ModeValidators.ControllerModels;

namespace Kursova.ProgramConfigs
{
    public static class ProgramValidators
    {
        public static void AddModelValidators(IServiceCollection services)
        {
            services.AddScoped<IValidator<UserLoginModel>, UserLoginModelValidator>();
            services.AddScoped<IValidator<UserRegistrationModel>, UserRegistrationModelValidator>();
            services.AddScoped<IValidator<UserTenderOffersModel>, UserTenderOffersModelValidator>();
            services.AddScoped<IValidator<OfferActionModel>, OfferActionModelValidator>();
            services.AddScoped<IValidator<NewTenderModel>, NewTenderModelValidator>();
            services.AddScoped<IValidator<NewOfferModel>, NewOfferModelValidator>();
            services.AddScoped<IValidator<TenderInitialActionModel>, TenderInitialActionModelValidator>();
        }
    }
}
