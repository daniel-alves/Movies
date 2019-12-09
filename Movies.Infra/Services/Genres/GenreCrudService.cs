using System;
using System.Linq;
using System.Threading.Tasks;
using Movies.Domain;
using Movies.Domain.Entities;
using Movies.Framework.Services;
using Movies.Infra.Repositories.Common;
using Movies.Infra.Repositories.Genres;

namespace Movies.Infra.Services.Genres
{
    public class GenreCrudService : CrudService<Genre>, IGenreCrudService
    {
        private readonly ICommonRepository<Movie> _movieRepository;

        public GenreCrudService(IGenreRepository repository, ICommonRepository<Movie> movieRepository) 
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
        
        public override Genre Update(Genre entity)
        {
            var persisted = Get(entity.Id);

            persisted.Name = entity.Name;
            persisted.Active = entity.Active;

            return base.Update(persisted);
        }
    }
}
