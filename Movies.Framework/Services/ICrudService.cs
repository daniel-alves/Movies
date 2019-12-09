using Movies.Framework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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

        IQueryable<TEntity> GetAll();

        TEntity Get(long id);

        bool Exists(long id);

        bool CanDelete(long id);
    }
}
