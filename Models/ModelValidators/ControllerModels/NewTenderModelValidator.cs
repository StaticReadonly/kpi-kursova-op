using FluentValidation;
using Models.ControllerModels;

namespace Models.ModelValidators.ControllerModels
{
    public class NewTenderModelValidator : AbstractValidator<NewTenderModel>
    {
        public NewTenderModelValidator() 
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(m => m.Description)
                .NotEmpty()
                .MaximumLength(512);

            RuleFor(m => m.Cost)
                .NotEmpty()
                .PrecisionScale(20, 2, true)
                .GreaterThan(0.0m);
        }
    }
}
