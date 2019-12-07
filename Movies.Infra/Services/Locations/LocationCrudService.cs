using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities;
using Movies.Infra.Repositories.Common;
using Movies.Infra.Services.Common;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Infra.Services.Locations
{
    public class LocationCrudService : CommonCrudService<Location>, ILocationCrudService
    {
        public LocationCrudService(ICommonRepository<Location> repository) 
            : base(repository)
        {
        }

        public override async Task<Location> GetByIdAsync(long id)
        {
            return base.GetAll().Include(e => e.Movies)
                .ThenInclude(e => e.Movie).FirstOrDefault(e => e.Id == id);
            
        }

        public override async Task<Location> Insert(Location entity)
        {
            entity.LocatedAt = DateTime.Now;

            return await base.Insert(entity);
        }

        public override async Task<Location> Update(Location entity)
        {
            var persisted = await GetByIdAsync(entity.Id);

            persisted.Cpf = entity.Cpf;
            persisted.Movies = entity.Movies;

            return await base.Update(persisted);
        }
    }
}
