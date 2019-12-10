using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities;
using Movies.Infra.Data.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public Task<Movie> GetByIdWithGenreAsync(long id)
        {
            return DbSet.Include(e => e.Genre).FirstOrDefaultAsync(e => e.Id == id);
        }

        public List<Movie> GetPageWithGenre(int limit, int offset)
        {
            return DbSet.Include(e => e.Genre).Take(limit).Skip(offset).ToList();
        }
    }
}
