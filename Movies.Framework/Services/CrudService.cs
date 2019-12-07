using Microsoft.EntityFrameworkCore;
using Movies.Framework.Entities;
using Movies.Framework.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Framework.Services
{
    public abstract class CrudService<TEntity, TContext> : ICrudService<TEntity, TContext>
        where TEntity : Entity
        where TContext : DbContext
    {
        private readonly IRepository<TEntity, TContext> _repository;

        protected CrudService(IRepository<TEntity, TContext> repository)
        {
            _repository = repository;
        }

        public virtual async Task<TEntity> GetByIdAsync(long id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public virtual async Task<TEntity> Insert(TEntity entity)
        {
            _repository.Add(entity);
            await _repository.SaveChangesAsync();

            return entity;
        }

        public virtual bool Insert(IEnumerable<TEntity> items)
        {
            var itemsList = items.ToList();

            var result = _repository.Insert(itemsList);

            _repository.SaveChanges();

            return result;
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            _repository.Update(entity);
            
            await _repository.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> Update(IEnumerable<TEntity> entities)
        {
            _repository.Update(entities);
            await _repository.SaveChangesAsync();

            return entities;
        }

        public virtual async Task<TEntity> Save(TEntity entity)
        {
            if (entity.Id > 0)
                return await Update(entity);

            return await Insert(entity);
        }

        public virtual async Task Delete(long id)
        {
            _repository.Remove(id);
            
            await _repository.SaveChangesAsync();
        }

        public virtual async Task Delete(Func<TEntity, bool> where)
        {
            _repository.RemoveRange(where);
            await _repository.SaveChangesAsync();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            var retorno = _repository.GetAll();
            return retorno;
        }

        public virtual TEntity Get(long id)
        {
            var retorno = _repository.Get(id);
            return retorno;
        }

        public virtual bool Exists(long id) {
            return _repository.Exists(id);
        }
    }
}
