using Movies.Domain.Entities;
using Movies.Framework.Services;
using System.Collections.Generic;

namespace Movies.Infra.Services.Movies
{
    public interface IMovieCrudService : ICrudService<Movie>
    {
        List<Movie> GetAllActiveAndContainName(string name);
    }
}
