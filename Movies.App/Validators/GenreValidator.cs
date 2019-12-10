using FluentValidation;
using Movies.App.Models.Genres;
using Movies.Infra.Repositories.Genres;

namespace Movies.App.Validators
{
    //validações, os erros ficam no model state
    public class GenreValidator : AbstractValidator<GenreViewModel>
    {
        private readonly IGenreRepository _genreRepository;

        public GenreValidator(IGenreRepository genreRepository)
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Obrigatório")
                .MaximumLength(50)
                .Must(BeUniqueName).WithMessage("Já esta cadastrado.");

            _genreRepository = genreRepository;
        }

        //garante que o nome do genêro seja único
        private bool BeUniqueName(GenreViewModel data, string name)
        {
            var genre = _genreRepository.GetByName(name);
            
            return genre == null || genre.Id == data.Id;
        }
    }
}
