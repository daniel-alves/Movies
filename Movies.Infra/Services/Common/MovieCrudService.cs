using Movies.Framework.Entities;
using Movies.Framework.Services;
using Movies.Infra.Contexts;
using Movies.Infra.Repositories.Common;

namespace Movies.Infra.Services.Common
{
    public class MovieCrudService<TEntity> : CrudService<TEntity, MovieContext>, IMovieCrudService<TEntity>
        where TEntity : Entity
    {
        public MovieCrudService(IMovieRepository<TEntity> repository) 
            : base(repository)
        {
        }
    }
}
