using FluentValidation;
using Movies.Domain;
using Movies.Infra.Services.Common;
using System.Linq;

namespace Movies.Infra.Validators
{
    public class GenreValidator : AbstractValidator<Genre>
    {
        private readonly IMovieCrudService<Genre> _genreService;

        public GenreValidator(IMovieCrudService<Genre> genreService)
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(50)
                .Must(BeUniqueName).WithMessage("Este genêro ja esta cadastrado.");

            _genreService = genreService;
        }

        private bool BeUniqueName(Genre data, string name)
        {
            var genre = _genreService.GetAll()
                .FirstOrDefault(e => e.Name == data.Name && e.Id != data.Id);

            return genre == null;
        }
    }
}
