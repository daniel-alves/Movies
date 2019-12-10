using Movies.Domain.Entities;
using Movies.Framework.Services;
using Movies.Infra.Repositories.Locations;
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
        
        //a locação sempre pode ser excluida não há tabelas que dependam dela atualmente
        public override bool CanDelete(long id) => true;
        
        //busca a locação com os filmes pelo id
        public override async Task<Location> GetByIdAsync(long id)
        {
            return _repository.GetByIdWithMovies(id);
        }
        
        public override async Task<Location> Insert(Location entity)
        {
            entity.LocatedAt = DateTime.Now;

            return await base.Insert(entity);
        }

        //atualiza a locação mantendo os dados não editáveis inalterados
        public override Location Update(Location entity)
        {
            var persisted = _repository.GetByIdWithMovies(entity.Id);

            persisted.Cpf = entity.Cpf;
            persisted.Movies = entity.Movies;

            return base.Update(persisted);
        }
    }
}
