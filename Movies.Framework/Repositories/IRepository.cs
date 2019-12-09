using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Framework.Repositories
{
    public interface IRepository<TEntity>
       where TEntity : class
    {
        void Add(TEntity entity);

        Task AddAsync(TEntity obj);

        Task<TEntity> GetByIdAsync(long id);

        TEntity Get(long id);

        List<TEntity> GetPage(int limit, int offset);

        void Update(TEntity entity);

        void Remove(long id);

        bool Add(IEnumerable<TEntity> items);

        bool Update(IEnumerable<TEntity> entities);

        bool Exists(long id);
    }
}
