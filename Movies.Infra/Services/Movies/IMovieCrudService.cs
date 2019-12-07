using Movies.Domain.Entities;
using Movies.Infra.Services.Common;

namespace Movies.Infra.Services.Movies
{
    public interface IMovieCrudService : ICommonCrudService<Movie>
    {
    }
}
