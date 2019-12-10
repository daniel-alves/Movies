using FluentValidation;
using Movies.App.Models.Locations;

namespace Movies.Infra.Validators
{
    //validações, os erros ficam no model state
    public class LocationValidator : AbstractValidator<LocationViewModel>
    {
        public LocationValidator()
        {
            RuleFor(e => e.Cpf)
                .NotEmpty().WithMessage("Obrigatório.")
                .Length(14);
        }
    }
}
