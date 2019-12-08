using FluentValidation;
using Movies.App.Models.Genres;
using Movies.Infra.Services.Genres;
using System.Linq;

namespace Movies.App.Validators
{
    public class GenreValidator : AbstractValidator<GenreViewModel>
    {
        private readonly IGenreCrudService _genreService;

        public GenreValidator(IGenreCrudService genreService)
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Obrigatório")
                .MaximumLength(50)
                .Must(BeUniqueName).WithMessage("Já esta cadastrado.");

            _genreService = genreService;
        }

        private bool BeUniqueName(GenreViewModel data, string name)
        {
            var genre = _genreService.GetAll()
                .FirstOrDefault(e => e.Name == data.Name && e.Id != data.Id);

            return genre == null;
        }
    }
}
