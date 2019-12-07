using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Framework.Repositories.Base
{
    public interface IRepository<TEntity, TContext>
       where TEntity : class
       where TContext : DbContext
    {
        void Add(TEntity entity);

        Task AddAsync(TEntity obj);

        Task<TEntity> GetByIdAsync(long id);

        TEntity Get(long id);

        IQueryable<TEntity> GetAll();

        void Update(TEntity entity);

        void Remove(long id);

        void RemoveRange(Func<TEntity, bool> where);

        Task<int> SaveChangesAsync();

        int SaveChanges();

        bool Add(IEnumerable<TEntity> items);

        bool Update(IEnumerable<TEntity> entities);

        bool Exists(long id);
    }
}
