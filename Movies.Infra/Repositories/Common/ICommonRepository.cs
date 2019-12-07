using Movies.Framework.Entities;
using Movies.Framework.Repositories.Base;
using Movies.Infra.Contexts;

namespace Movies.Infra.Repositories.Common
{

    public interface ICommonRepository<TEntity> : IRepository<TEntity, MovieContext>
        where TEntity : Entity
    {
    }
}
