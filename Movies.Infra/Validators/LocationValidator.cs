using FluentValidation;
using Movies.Domain.Entities;

namespace Movies.Infra.Validators
{
    public class LocationValidator : AbstractValidator<Location>
    {
        public LocationValidator()
        {
            RuleFor(e => e.Cpf)
                .NotEmpty()
                .Length(14);
        }
    }
}
