using Microsoft.EntityFrameworkCore;
using Movies.Framework.Entities;
using Movies.Framework.Services;
using Movies.Infra.Contexts;

namespace Movies.Infra.Services.Common
{
    public interface IMovieCrudService<TEntity> : ICrudService<TEntity, MovieContext> 
        where TEntity : Entity
    {
    }
}
