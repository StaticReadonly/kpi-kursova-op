using FluentValidation;
using Models.ControllerModels;

namespace Models.ModelValidators.ControllerModels
{
    public class UserTenderOffersModelValidator : AbstractValidator<UserTenderOffersModel>
    {
        public UserTenderOffersModelValidator()
        {
            RuleFor(m => m.Id)
                .NotEmpty();
        }
    }
}
