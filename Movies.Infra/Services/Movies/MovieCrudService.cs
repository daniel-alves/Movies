using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Domain.Entities;
using Movies.Framework.Services;
using Movies.Infra.Repositories.Common;
using Movies.Infra.Repositories.Movies;

namespace Movies.Infra.Services.Movies
{
    public class MovieCrudService : CrudService<Movie>, IMovieCrudService
    {
        protected readonly IMovieRepository _movieRepository;

        private readonly ICommonRepository<MovieLocation> _movieLocationRepository;
        
        public MovieCrudService(IMovieRepository repository, ICommonRepository<MovieLocation> movieLocationRepository) 
            : base(repository)
        {
            _movieRepository = repository;
            _movieLocationRepository = movieLocationRepository;
        }

        //refatorar
        public override bool CanDelete(long id)
        {
            //var location = _movieLocationRepository.GetAll()
            //    .FirstOrDefault(e => e.MovieId == id);

            //return location == null;
            return false;
        }

        //public override List<Movie> GetPage(int limit, int offset)
        //{
        //    return base.GetPage(limit, offset).Include(e => e.Genre);
        //}

        //public override async Task<Movie> GetByIdAsync(long id)
        //{
        //    return base.GetAll().Include(e => e.Genre).FirstOrDefault(e => e.Id == id);
        //}

        public override async Task<Movie> Insert(Movie entity)
        {
            entity.CreatedAt = DateTime.Now;

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
            return _movieRepository.GetAllActiveAndContainName(name);
        }
    }
}
