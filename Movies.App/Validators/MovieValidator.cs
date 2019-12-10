using FluentValidation;
using Movies.App.Models.Movies;
using Movies.Infra.Repositories.Genres;
using Movies.Infra.Repositories.Movies;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Infra.Validators
{
    //validações, os erros ficam no model state
    public class MovieValidator : AbstractValidator<MovieViewModel>
    {
        private readonly IMovieRepository _movieRepository;

        private readonly IGenreRepository _genreRepository;

        public MovieValidator(IGenreRepository genreRepository, IMovieRepository movieRepository)
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .MaximumLength(150)
                .Must(BeUniqueName).WithMessage("Já foi cadastrado.");

            RuleFor(e => e.GenreId)
                .Must(Exists).WithMessage("Obrigatório.")
                .MustAsync(BeActive).WithMessage("O genêro deve estar ativo.");

            _genreRepository = genreRepository;
            _movieRepository = movieRepository;
        }

        //garante que sómente genêros ativos sejam vinculados
        private async Task<bool> BeActive(long genreId, CancellationToken token)
        {
            var genre = await _genreRepository.GetByIdAsync(genreId);

            return genre != null && genre.Active;
        }

        //garante que o id de genêro recebido esteja cadastrado
        private bool Exists(long genreId)
        { 
            return _genreRepository.Exists(genreId);
        }

        //garante que o nome do filme seja único
        private bool BeUniqueName(MovieViewModel data, string name)
        {
            var movie = _movieRepository.GetByName(name);

            return movie == null || movie.Id == data.Id;
        }
    }
}
