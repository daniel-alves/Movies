using Movies.Domain;
using Movies.Framework.Repositories;
using System.Collections.Generic;

namespace Movies.Infra.Repositories.Genres
{
    public interface IGenreRepository : IRepository<Genre>
    {
        Genre GetByName(string genreName);

        List<Genre> GetAllActiveAndContainName(string name);
    }
}
