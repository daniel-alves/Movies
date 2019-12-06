using Microsoft.EntityFrameworkCore;
using Movies.Framework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Framework.Services
{
    public interface ICrudService<TEntity, TContext>
        where TEntity : Entity
        where TContext : DbContext
    {
        Task<TEntity> GetByIdAsync(long id);

        Task<TEntity> Insert(TEntity entity);

        bool Insert(IEnumerable<TEntity> items);

        Task<TEntity> Update(TEntity entity);

        Task<IEnumerable<TEntity>> Update(IEnumerable<TEntity> entities);

        Task<TEntity> Save(TEntity entity);

        Task Delete(long id);

        Task Delete(Func<TEntity, bool> where);

        IQueryable<TEntity> GetAll();

        TEntity Get(long id);

        bool Exists(long id);
    }
}
