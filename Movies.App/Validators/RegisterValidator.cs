using FluentValidation;
using Movies.App.Models.Accounts;

namespace Movies.App.Validators
{
    //validações, os erros ficam no model state
    public class RegisterValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterValidator()
        {
            RuleFor(e => e.Email).NotEmpty().EmailAddress();

            RuleFor(e => e.Password).NotEmpty();

            RuleFor(e => e.ConfirmPassword)
                .NotEmpty()
                .Equal(e => e.Password).WithMessage("A senha e a confirmação devem ser iguais.");
        }
    }
}
