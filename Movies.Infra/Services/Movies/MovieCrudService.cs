using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities;
using Movies.Framework.Services;
using Movies.Infra.Data.Contexts;
using Movies.Infra.Repositories.Common;

namespace Movies.Infra.Services.Movies
{
    public class MovieCrudService : CrudService<Movie>, IMovieCrudService
    {
        private readonly ICommonRepository<MovieLocation> _movieLocationRepository;

        public MovieCrudService(ICommonRepository<Movie> repository, ICommonRepository<MovieLocation> movieLocationRepository) 
            : base(repository)
        {
            _movieLocationRepository = movieLocationRepository;
        }

        public override bool CanDelete(long id)
        {
            var location = _movieLocationRepository.GetAll()
                .FirstOrDefault(e => e.MovieId == id);

            return location == null;
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

        public override Movie Update(Movie entity)
        {
            var persisted = Get(entity.Id);

            persisted.Name = entity.Name;
            persisted.Active = entity.Active;
            persisted.GenreId = entity.GenreId;

            return base.Update(persisted);
        }
    }
}
