using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities;
using Movies.Infra.Data.Contexts;
using System.Linq;

namespace Movies.Infra.Repositories.Locations
{
    //implementa o ef core repository e permite extensão. OCP aberta a extensão e fechada a modificação
    public class LocationRepository : EfCoreRepository<Location, MovieContext>, ILocationRepository
    {
        public LocationRepository(MovieContext context) : base(context)
        {
        }

        //busca a locação, com os serus filmes, pelo id da locação
        public Location GetByIdWithMovies(long id)
        {
            return DbSet.Include(e => e.Movies)
                .ThenInclude(e => e.Movie)
                .FirstOrDefault(e => e.Id == id);
        }
    }
}
