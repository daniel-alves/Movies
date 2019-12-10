using Movies.Domain.Entities;
using Movies.Framework.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Infra.Repositories.Movies
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Movie GetByName(string name);

        Movie GetByGenreId(long genreId);

        List<Movie> GetAllActiveAndContainName(string name);

        Task<Movie> GetByIdWithGenreAsync(long id);

        List<Movie> GetPageWithGenre(int limit, int offset);
    }
}
