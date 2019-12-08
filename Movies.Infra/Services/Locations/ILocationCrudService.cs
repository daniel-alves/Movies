using Movies.Domain.Entities;
using Movies.Framework.Services;
using Movies.Infra.Data.Contexts;

namespace Movies.Infra.Services.Locations
{
    public interface ILocationCrudService : ICrudService<Location, MovieContext>
    {
    }
}
