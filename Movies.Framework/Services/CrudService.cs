using Movies.Framework.Entities;
using Movies.Framework.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Framework.Services
{

    //O service serve como um proxy para os repositórios oferece um API pública comun para todos os repositórios mantendo a flexibilidade
    public abstract class CrudService<TEntity> : ICrudService<TEntity>
        where TEntity : Entity
    {
        private readonly IRepository<TEntity> _repository;

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

        //cria uma nova entity ou atualiza caso ja exista
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

        //permite a quem implementar verificar se é possível ou não excluir uma entity pelo id
        //é abstrata pois assim o dev que implementar a classe está ciente que deve implementar o método conforme sua necessidade,
        //se apenas retornasse true por padrão um dev desavisado acabaria por herdar e permitir a exclusão sem saber da existência e
        //utilidade desse método
        public abstract bool CanDelete(long id);

        public List<TEntity> GetAllById(long[] ids)
        {
            return _repository.GetAllById(ids);
        }
    }
}
