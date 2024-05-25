using FluentValidation;
using Models.ControllerModels;

namespace Models.ModelValidators.ControllerModels
{
    public class UserLoginModelValidator 
        : AbstractValidator<UserLoginModel>
    {
        public UserLoginModelValidator() 
        {
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
