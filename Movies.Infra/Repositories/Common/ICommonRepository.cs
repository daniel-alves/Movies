using Movies.Framework.Entities;
using Movies.Framework.Repositories;

namespace Movies.Infra.Repositories.Common
{

    public interface ICommonRepository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
    }
}
