using System;
using Movies.Domain;
using System.Threading.Tasks;
using Movies.Framework.Services;
using Movies.Infra.Repositories.Genres;
using Movies.Infra.Repositories.Movies;
using System.Collections.Generic;

namespace Movies.Infra.Services.Genres
{
    public class GenreCrudService : CrudService<Genre>, IGenreCrudService
    { 
        private readonly IGenreRepository _genreRepository;

        private readonly IMovieRepository _movieRepository;

        public GenreCrudService(IGenreRepository repository, IMovieRepository movieRepository) 
            : base(repository)
        {
            _genreRepository = repository;
            _movieRepository = movieRepository;
        }

        public override bool CanDelete(long id)
        {
            var movie = _movieRepository.GetByGenreId(id);
            
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

        public List<Genre> GetAllActiveAndContainName(string name)
        {
            return _genreRepository.GetAllActiveAndContainName(name);
        }
    }
}
