using FluentValidation;
using Movies.App.Models.Accounts;

namespace Movies.App.Validators
{
    public class LoginValidator : AbstractValidator<LoginViewModel>
    {
        public LoginValidator()
        {
            RuleFor(e => e.Email).NotEmpty().EmailAddress();
            RuleFor(e => e.Password).NotEmpty();
        }
    }
}
