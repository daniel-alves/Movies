using FluentValidation;
using Movies.App.Models.Movies;
using Movies.Infra.Services.Genres;
using Movies.Infra.Services.Movies;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Infra.Validators
{
    public class MovieValidator : AbstractValidator<MovieViewModel>
    {
        private readonly IMovieCrudService _movieService;

        private readonly IGenreCrudService _genreService;

        public MovieValidator(IGenreCrudService genreService, IMovieCrudService movieService)
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .MaximumLength(150)
                .Must(BeUniqueName).WithMessage("Já foi cadastrado.");

            RuleFor(e => e.GenreId)
                .Must(Exists).WithMessage("Obrigatório.")
                .MustAsync(BeActive).WithMessage("O genêro deve estar ativo.");

            _genreService = genreService;
            _movieService = movieService;
        }

        private async Task<bool> BeActive(long genreId, CancellationToken token)
        {
            var genre = await _genreService.GetByIdAsync(genreId);

            return genre != null && genre.Active;
        }

        private bool Exists(long genreId)
        { 
            return _genreService.Exists(genreId);
        }

        private bool BeUniqueName(MovieViewModel data, string name)
        {
            var genre = _movieService.GetAll()
                .FirstOrDefault(e => e.Name == data.Name && e.Id != data.Id);

            return genre == null;
        }
    }
}
