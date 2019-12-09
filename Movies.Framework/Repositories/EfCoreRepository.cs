using System.Linq;
using System.Threading.Tasks;
using Movies.Framework.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Movies.Framework.Repositories;

namespace Movies.Infra.Repositories
{
    public class EfCoreRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : Entity
        where TContext : DbContext
    {
        private readonly TContext _context;

        protected readonly DbSet<TEntity> DbSet;

        public EfCoreRepository(TContext context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();
        }

        public virtual void Add(TEntity obj)
        {
            DbSet.Add(obj);
            _context.SaveChanges();
        }

        public async Task AddAsync(TEntity obj)
        {
            await DbSet.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public virtual Task<TEntity> GetByIdAsync(long id)
            => DbSet.FindAsync(id);

        public virtual TEntity Get(long id)
            => DbSet.Find(id);

        public virtual IQueryable<TEntity> GetAll()
            => DbSet;

        public virtual void Update(TEntity obj)
        {
            DbSet.Update(obj);
            _context.SaveChanges();
        }

        public bool Exists(long id)
        {
            if (id == 0) return false;

            return DbSet.Any(e => e.Id == id);
        }

        public void Remove(long id)
        {
            var entity = DbSet.Find(id);

            if (entity != null)
            {
                DbSet.Remove(entity);
                _context.SaveChanges();
            }
        }

        public bool Add(IEnumerable<TEntity> items)
        {
            var list = items.ToList();
            foreach (var item in list)
            {
                Add(item);
            }
            _context.SaveChanges();
            return true;
        }

        public bool Update(IEnumerable<TEntity> entities)
        {
            var list = entities.ToList();

            foreach (var item in list)
            {
                Update(item);
            }
            _context.SaveChanges();
            return true;
        }
    }
}
