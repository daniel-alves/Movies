using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities;
using Movies.Infra.Data.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Infra.Repositories.Movies
{
    //herda o ef core e implementa suas particularidades
    public class MovieRepository : EfCoreRepository<Movie, MovieContext>, IMovieRepository
    {
        public MovieRepository(MovieContext context) 
            : base(context)
        {
        }

        //busca qualquer filme pelo id do genêro normalmente é usada para verificar sem o genêro está em uso
        public Movie GetByGenreId(long genreId)
        {
            return DbSet.FirstOrDefault(e => e.GenreId == genreId);
        }

        //busca todos os filmes ativos que contranham name no nome do filme
        public List<Movie> GetAllActiveAndContainName(string name)
        {
            return DbSet.Where(e => e.Name.Contains(name) && e.Active).ToList();
        }

        //busca um filme pelo nome, para verificar se o filme ja esta cadastrado
        public Movie GetByName(string name)
        {
            return DbSet.FirstOrDefault(e => e.Name == name);
        }

        //busca um filme pelo id carrega seu genêro de forma assincrona
        public Task<Movie> GetByIdWithGenreAsync(long id)
        {
            return DbSet.Include(e => e.Genre).FirstOrDefaultAsync(e => e.Id == id);
        }

        //busca uma página de filmes com os seus genêros
        public List<Movie> GetPageWithGenre(int limit, int offset)
        {
            return DbSet.Include(e => e.Genre).Take(limit).Skip(offset).ToList();
        }
    }
}
