using Movies.Domain.Entities;
using Movies.Infra.Data.Contexts;
using System.Linq;

namespace Movies.Infra.Repositories.MovieLocations
{
    //herda o ef core e implementa as particularidades
    public class MovieLocationRepository : EfCoreRepository<MovieLocation, MovieContext>, IMovieLocationRepository
    {
        public MovieLocationRepository(MovieContext context) : base(context)
        {
        }

        //busca uma movie locação pelo id do filme
        public MovieLocation GetByMovieId(long movieId)
        {
            return DbSet.FirstOrDefault(e => e.MovieId == movieId);
        }
    }
}
