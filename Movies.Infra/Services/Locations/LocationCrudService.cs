using Movies.Domain.Entities;
using Movies.Framework.Services;
using Movies.Infra.Repositories.Locations;
using Movies.Infra.Repositories.MovieLocations;
using Movies.Infra.Repositories.Movies;
using System;
using System.Threading.Tasks;

namespace Movies.Infra.Services.Locations
{
    public class LocationCrudService : CrudService<Location>, ILocationCrudService
    {

        private readonly ILocationRepository _repository;

        public LocationCrudService(ILocationRepository repository) 
            : base(repository)
        {
            _repository = repository;
        }
        
        public override bool CanDelete(long id) => true;
        
        public override async Task<Location> GetByIdAsync(long id)
        {
            return _repository.GetByIdWithMovies(id);
        }

        public override async Task<Location> Insert(Location entity)
        {
            entity.LocatedAt = DateTime.Now;

            return await base.Insert(entity);
        }

        public override Location Update(Location entity)
        {
            var persisted = _repository.GetByIdWithMovies(entity.Id);

            persisted.Cpf = entity.Cpf;
            persisted.Movies = entity.Movies;

            return base.Update(persisted);
        }
    }
}
