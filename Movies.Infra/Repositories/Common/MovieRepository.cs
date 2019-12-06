using Movies.Framework.Entities;
using Movies.Infra.Contexts;

namespace Movies.Infra.Repositories.Common
{
    public class MovieRepository<TEntity> : Repository<TEntity, MovieContext>, IMovieRepository<TEntity>
        where TEntity : Entity
    {
        public MovieRepository(MovieContext context) : base(context)
        {
        }
    }
}
