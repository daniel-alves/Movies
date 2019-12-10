using Movies.Domain.Entities;
using Movies.Infra.Data.Contexts;
using System.Linq;

namespace Movies.Infra.Repositories.MovieLocations
{
    public class MovieLocationRepository : EfCoreRepository<MovieLocation, MovieContext>, IMovieLocationRepository
    {
        public MovieLocationRepository(MovieContext context) : base(context)
        {
        }

        public MovieLocation GetByMovieId(long movieId)
        {
            return DbSet.FirstOrDefault(e => e.MovieId == movieId);
        }
    }
}
