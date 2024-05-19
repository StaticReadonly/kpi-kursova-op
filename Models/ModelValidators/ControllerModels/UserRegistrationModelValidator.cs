using FluentValidation;
using Models.ControllerModels;

namespace Models.ModeValidators.ControllerModels
{
    public class UserRegistrationModelValidator
        : AbstractValidator<UserRegistrationModel>
    {
        public UserRegistrationModelValidator() 
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100);

            RuleFor(m => m.Surname)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100); ;

            RuleFor(m => m.Patronimyc)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100);

            RuleFor(m => m.Address)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(100);

            RuleFor(m => m.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100);

            RuleFor(m => m.Password)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(100);
        }
    }
}
