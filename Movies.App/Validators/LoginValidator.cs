using FluentValidation;
using Movies.App.Models.Accounts;

namespace Movies.App.Validators
{
    //validações, os erros ficam no model state
    public class LoginValidator : AbstractValidator<LoginViewModel>
    {
        public LoginValidator()
        {
            RuleFor(e => e.Email).NotEmpty().EmailAddress();
            RuleFor(e => e.Password).NotEmpty();
        }
    }
}
