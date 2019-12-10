using Movies.Domain;
using Movies.Framework.Services;
using System.Collections.Generic;

namespace Movies.Infra.Services.Genres
{
    public interface IGenreCrudService : ICrudService<Genre>
    {
        List<Genre> GetAllActiveAndContainName(string name);
    }
}
