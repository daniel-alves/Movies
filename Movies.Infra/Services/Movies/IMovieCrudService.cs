using Movies.Domain.Entities;
using Movies.Framework.Services;
using Movies.Infra.Data.Contexts;

namespace Movies.Infra.Services.Movies
{
    public interface IMovieCrudService : ICrudService<Movie>
    {
    }
}
