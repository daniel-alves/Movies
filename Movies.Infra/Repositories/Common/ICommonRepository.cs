using Movies.Framework.Entities;
using Movies.Framework.Repositories;
using Movies.Infra.Data.Contexts;

namespace Movies.Infra.Repositories.Common
{

    public interface ICommonRepository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
    }
}
