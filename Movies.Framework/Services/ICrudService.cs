using Movies.Framework.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Framework.Services
{
    public interface ICrudService<TEntity>
        where TEntity : Entity
    {
        Task<TEntity> GetByIdAsync(long id);

        Task<TEntity> Insert(TEntity entity);

        bool Insert(IEnumerable<TEntity> items);

        TEntity Update(TEntity entity);

        IEnumerable<TEntity> Update(IEnumerable<TEntity> entities);

        Task<TEntity> Save(TEntity entity);

        void Delete(long id);

        List<TEntity> GetPage(int limit, int offset);

        TEntity Get(long id);

        bool Exists(long id);

        bool CanDelete(long id);
    }
}
