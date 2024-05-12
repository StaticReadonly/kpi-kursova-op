using FluentValidation;
using Models.ControllerModels;

namespace Models.ModelValidators.ControllerModels
{
    public class TenderInitialActionModelValidator : AbstractValidator<TenderInitialActionModel>
    {
        public TenderInitialActionModelValidator()
        {
            RuleFor(x => x.TenderId)
                .NotEmpty();

            RuleFor(x => x.Action)
                .NotEmpty();
        }
    }
}
