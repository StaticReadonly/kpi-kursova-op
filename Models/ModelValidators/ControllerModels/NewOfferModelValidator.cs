using FluentValidation;
using Models.ControllerModels;

namespace Models.ModelValidators.ControllerModels
{
    public class NewOfferModelValidator : AbstractValidator<NewOfferModel>
    {
        public NewOfferModelValidator() 
        {
            RuleFor(m => m.TenderId)
                .NotEmpty();

            RuleFor(m => m.Price)
                .NotEmpty()
                .PrecisionScale(20, 2, true)
                .GreaterThan(0);

            RuleFor(m => m.Description)
                .NotEmpty()
                .MaximumLength(512);
        }
    }
}
