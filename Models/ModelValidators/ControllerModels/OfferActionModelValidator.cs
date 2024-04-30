using FluentValidation;
using Models.ControllerModels;

namespace Models.ModelValidators.ControllerModels
{
    public class OfferActionModelValidator : AbstractValidator<OfferActionModel>
    {
        public OfferActionModelValidator()
        {
            RuleFor(m => m.TenderId)
                .NotEmpty();

            RuleFor(m => m.OfferId)
                .NotEmpty();

            RuleFor(m => m.Action)
                .NotEmpty();
        }
    }
}
