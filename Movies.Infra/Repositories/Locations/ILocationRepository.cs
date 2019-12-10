using Movies.Domain.Entities;
using Movies.Framework.Repositories;

namespace Movies.Infra.Repositories.Locations
{
    public interface ILocationRepository : IRepository<Location>
    {
        Location GetByIdWithMovies(long id);
    }
}
