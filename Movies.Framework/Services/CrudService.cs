using Microsoft.EntityFrameworkCore;
using Movies.Framework.Entities;
using Movies.Framework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Framework.Services
{
    public abstract class CrudService<TEntity> : ICrudService<TEntity>
        where TEntity : Entity
    {
        protected readonly IRepository<TEntity> _repository;

        protected CrudService(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual async Task<TEntity> GetByIdAsync(long id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public virtual async Task<TEntity> Insert(TEntity entity)
        {
            await _repository.AddAsync(entity);
            return entity;
        }

        public virtual bool Insert(IEnumerable<TEntity> items)
        {
            var itemsList = items.ToList();

            var result = _repository.Add(itemsList);

            return result;
        }

        public virtual TEntity Update(TEntity entity)
        {
            _repository.Update(entity);
            
            return entity;
        }

        public virtual IEnumerable<TEntity> Update(IEnumerable<TEntity> entities)
        {
            _repository.Update(entities);

            return entities;
        }

        public virtual async Task<TEntity> Save(TEntity entity)
        {
            if (entity.Id > 0)
                return Update(entity);

            return await Insert(entity);
        }

        public virtual void Delete(long id)
        {
            _repository.Remove(id);
        }

        public virtual List<TEntity> GetPage(int limit, int offset)
        {
            var retorno = _repository.GetPage(limit, offset);
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

        public abstract bool CanDelete(long id);
    }
}
