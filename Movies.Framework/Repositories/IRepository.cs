using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Framework.Repositories
{
    //contrato de repositório padrão a forma de acessar os dados pode ser diferente no caso foi utilizado dapper e ef core
    //podem ser adicionados outros conforme a necessidade desta forma é possível mudar de ORM facilmente desde que seja utilizado 
    //o repository pattern com este contrato
    public interface IRepository<TEntity>
       where TEntity : class
    {
        void Add(TEntity entity);

        Task AddAsync(TEntity obj);

        Task<TEntity> GetByIdAsync(long id);

        TEntity Get(long id);

        List<TEntity> GetPage(int limit, int offset);

        List<TEntity> GetAllById(long[] ids);

        void Update(TEntity entity);

        void Remove(long id);

        bool Add(IEnumerable<TEntity> items);

        bool Update(IEnumerable<TEntity> entities);

        bool Exists(long id);
    }
}
