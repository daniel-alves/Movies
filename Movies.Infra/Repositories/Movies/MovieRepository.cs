using Movies.Domain.Entities;
using Movies.Infra.Data.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace Movies.Infra.Repositories.Movies
{
    public class MovieRepository : EfCoreRepository<Movie, MovieContext>, IMovieRepository
    {
        public MovieRepository(MovieContext context) 
            : base(context)
        {
        }

        public Movie GetByGenreId(long genreId)
        {
            return DbSet.FirstOrDefault(e => e.GenreId == genreId);
        }

        public List<Movie> GetAllActiveAndContainName(string name)
        {
            return DbSet.Where(e => e.Name.Contains(name) && e.Active).ToList();
        }

        public Movie GetByName(string name)
        {
            return DbSet.FirstOrDefault(e => e.Name == name);
        }
    }
}
