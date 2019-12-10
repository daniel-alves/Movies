using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Domain.Entities;
using Movies.Framework.Services;
using Movies.Infra.Repositories.MovieLocations;
using Movies.Infra.Repositories.Movies;

namespace Movies.Infra.Services.Movies
{
    public class MovieCrudService : CrudService<Movie>, IMovieCrudService
    {
        protected readonly IMovieRepository _repository;

        private readonly IMovieLocationRepository _movieLocationRepository;
        
        public MovieCrudService(IMovieRepository repository, IMovieLocationRepository movieLocationRepository) 
            : base(repository)
        {
            _repository = repository;
            _movieLocationRepository = movieLocationRepository;
        }
        
        public override bool CanDelete(long id)
        {
            var location = _movieLocationRepository.GetByMovieId(id);

            return location == null;
        }

        public override List<Movie> GetPage(int limit, int offset)
        {
            return _repository.GetPageWithGenre(limit, offset);
        }

        public override async Task<Movie> GetByIdAsync(long id)
        {
            return await _repository.GetByIdWithGenreAsync(id);
        }

        public override async Task<Movie> Insert(Movie entity)
        {
            entity.CreatedAt = DateTime.Now;

            entity.Genre = null;

            return await base.Insert(entity);
        }

        public override Movie Update(Movie entity)
        {
            var persisted = Get(entity.Id);

            persisted.Name = entity.Name;
            persisted.Active = entity.Active;
            persisted.GenreId = entity.GenreId;

            return base.Update(persisted);
        }

        public List<Movie> GetAllActiveAndContainName(string name)
        {
            return _repository.GetAllActiveAndContainName(name);
        }
    }
}
