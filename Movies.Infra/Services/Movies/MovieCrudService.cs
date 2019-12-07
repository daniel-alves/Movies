using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities;
using Movies.Infra.Repositories.Common;
using Movies.Infra.Services.Common;

namespace Movies.Infra.Services.Movies
{
    public class MovieCrudService : CommonCrudService<Movie>, IMovieCrudService
    {
        public MovieCrudService(ICommonRepository<Movie> repository) : base(repository)
        {

        }

        public override IQueryable<Movie> GetAll()
        {
            return base.GetAll().Include(e => e.Genre);
        }

        public override async Task<Movie> GetByIdAsync(long id)
        {
            return base.GetAll().Include(e => e.Genre).FirstOrDefault(e => e.Id == id);
        }

        public override async Task<Movie> Insert(Movie entity)
        {
            entity.CreatedAt = DateTime.Now;

            return await base.Insert(entity);
        }

        public override async Task<Movie> Update(Movie entity)
        {
            var persisted = await GetByIdAsync(entity.Id);

            persisted.Name = entity.Name;
            persisted.Active = entity.Active;
            persisted.GenreId = entity.GenreId;

            return await base.Update(persisted);
        }
    }
}
