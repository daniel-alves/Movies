using Movies.Domain;
using Movies.Framework.Repositories;

namespace Movies.Infra.Repositories.Genres
{
    public interface IGenreRepository : IRepository<Genre>
    {
        Genre GetByName(string genreName);
    }
}
