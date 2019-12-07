using System;
using System.Threading.Tasks;
using Movies.Domain;
using Movies.Infra.Repositories.Common;
using Movies.Infra.Services.Common;

namespace Movies.Infra.Services.Genres
{
    public class GenreCrudService : CommonCrudService<Genre>, IGenreCrudService
    {
        public GenreCrudService(ICommonRepository<Genre> repository) 
            : base(repository)
        {
                
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
