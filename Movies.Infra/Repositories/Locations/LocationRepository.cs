using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities;
using Movies.Infra.Data.Contexts;
using System.Linq;

namespace Movies.Infra.Repositories.Locations
{
    public class LocationRepository : EfCoreRepository<Location, MovieContext>, ILocationRepository
    {
        public LocationRepository(MovieContext context) : base(context)
        {
        }

        public Location GetByIdWithMovies(long id)
        {
            return DbSet.Include(e => e.Movies)
                .ThenInclude(e => e.Movie)
                .FirstOrDefault(e => e.Id == id);
        }
    }
}
