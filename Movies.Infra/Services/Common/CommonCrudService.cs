using Movies.Framework.Entities;
using Movies.Framework.Services;
using Movies.Infra.Data.Contexts;
using Movies.Infra.Repositories.Common;

namespace Movies.Infra.Services.Common
{
    public class CommonCrudService<TEntity> : CrudService<TEntity, MovieContext>, ICommonCrudService<TEntity>
        where TEntity : Entity
    {
        public CommonCrudService(ICommonRepository<TEntity> repository) 
            : base(repository)
        {
        }
    }
}
