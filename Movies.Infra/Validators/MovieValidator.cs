using FluentValidation;
using Movies.Domain;
using Movies.Domain.Entities;
using Movies.Infra.Services.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Infra.Validators
{
    public class MovieValidator : AbstractValidator<Movie>
    {
        private readonly IMovieCrudService<Movie> _movieService;

        private readonly IMovieCrudService<Genre> _genreService;

        public MovieValidator(IMovieCrudService<Genre> genreService, IMovieCrudService<Movie> movieService)
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(150)
                .Must(BeUniqueName).WithMessage("Este filme já esta cadastrado.");

            RuleFor(e => e.GenreId)
                .NotEmpty()
                .Must(Exists).WithMessage("Genêro não encontrado.")
                .MustAsync(BeActive).WithMessage("O genêro esta inativo.");

            _genreService = genreService;
            _movieService = movieService;
        }

        private async Task<bool> BeActive(long genreId, CancellationToken token)
        {
            var genre = await _genreService.GetByIdAsync(genreId);

            return genre.Active;
        }

        private bool Exists(long genreId)
        {
            return _genreService.Exists(genreId);
        }

        private bool BeUniqueName(Movie data, string name)
        {
            var genre = _movieService.GetAll()
                .FirstOrDefault(e => e.Name == data.Name && e.Id != data.Id);

            return genre == null;
        }
    }
}
