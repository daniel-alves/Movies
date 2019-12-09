using Movies.Domain;
using Movies.Framework.Services;
using Movies.Infra.Data.Contexts;

namespace Movies.Infra.Services.Genres
{
    public interface IGenreCrudService : ICrudService<Genre>
    {
    }
}
