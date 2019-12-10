using Movies.Domain.Entities;
using Movies.Framework.Repositories;

namespace Movies.Infra.Repositories.MovieLocations
{
    public interface IMovieLocationRepository : IRepository<MovieLocation>
    {
        MovieLocation GetByMovieId(long movieId);
    }
}
