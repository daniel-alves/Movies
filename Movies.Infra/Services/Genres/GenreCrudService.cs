using System;
using System.Linq;
using System.Threading.Tasks;
using Movies.Domain;
using Movies.Domain.Entities;
using Movies.Framework.Services;
using Movies.Infra.Data.Contexts;
using Movies.Infra.Repositories.Common;

namespace Movies.Infra.Services.Genres
{
    public class GenreCrudService : CrudService<Genre, MovieContext>, IGenreCrudService
    {
        private readonly ICommonRepository<Movie> _movieRepository;

        public GenreCrudService(ICommonRepository<Genre> repository, ICommonRepository<Movie> movieRepository) 
            : base(repository)
        {
            _movieRepository = movieRepository;
        }

        public override bool CanDelete(long id)
        {
            var movie = _movieRepository.GetAll().FirstOrDefault(e => e.GenreId == id);

            return movie == null;
        }

        public override async Task<Genre> Insert(Genre entity)
        {
            entity.CreatedAt = DateTime.Now;

            return await base.Insert(entity);
        }
        
        public override async Task<Genre> Update(Genre entity)
        {
            var persisted = await GetByIdAsync(entity.Id);

            persisted.Name = entity.Name;
            persisted.Active = entity.Active;

            return await base.Update(persisted);
        }
    }
}
