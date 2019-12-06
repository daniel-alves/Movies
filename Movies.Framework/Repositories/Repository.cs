using Microsoft.EntityFrameworkCore;
using Movies.Framework.Entities;
using Movies.Framework.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Infra.Repositories
{
    public class Repository<TEntity, TContext> : IRepository<TEntity, TContext>
        where TEntity : Entity
        where TContext : DbContext
    {
        private readonly TContext _context;

        protected readonly DbSet<TEntity> DbSet;

        public Repository(TContext context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();
        }

        public virtual void Add(TEntity obj)
            => DbSet.Add(obj);

        public async Task AddAsync(TEntity obj)
            => await DbSet.AddAsync(obj);

        public virtual Task<TEntity> GetByIdAsync(long id)
            => DbSet.FindAsync(id);

        public virtual TEntity Get(long id)
            => DbSet.Find(id);

        public virtual IQueryable<TEntity> GetAll()
            => DbSet;

        public virtual void Update(TEntity obj) 
            => DbSet.Update(obj);

        public Task<int> SaveChangesAsync()
            => _context.SaveChangesAsync();

        public int SaveChanges() 
            => _context.SaveChanges();

        public bool Exists(long id)
            => DbSet.Any(e => e.Id == id);

        public void RemoveRange(Func<TEntity, bool> where)
            => DbSet.RemoveRange(DbSet.Where(where));

        public void Remove(long id)
        {
            var entity = DbSet.Find(id);

            if (entity != null)
                DbSet.Remove(entity);
        }

        public bool Insert(IEnumerable<TEntity> items)
        {
            var list = items.ToList();
            foreach (var item in list)
            {
                Add(item);
            }
            return true;
        }

        public bool Update(IEnumerable<TEntity> entities)
        {
            var list = entities.ToList();

            foreach (var item in list)
            {
                Update(item);
            }

            return true;
        }
    }
}
