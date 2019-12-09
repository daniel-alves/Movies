﻿using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities;
using Movies.Framework.Services;
using Movies.Infra.Data.Contexts;
using Movies.Infra.Repositories.Common;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Infra.Services.Locations
{
    public class LocationCrudService : CrudService<Location>, ILocationCrudService
    {
        public LocationCrudService(ICommonRepository<Location> repository) 
            : base(repository)
        {
        }
        
        public override bool CanDelete(long id) => true;
        
        public override async Task<Location> GetByIdAsync(long id)
        {
            return null;

            //Todo: refatorar
            //return base.GetAll().Include(e => e.Movies)
            //    .ThenInclude(e => e.Movie)
            //    .FirstOrDefault(e => e.Id == id);
        }

        public override async Task<Location> Insert(Location entity)
        {
            entity.LocatedAt = DateTime.Now;

            return await base.Insert(entity);
        }

        public override Location Update(Location entity)
        {
            var persisted = Get(entity.Id);

            persisted.Cpf = entity.Cpf;
            persisted.Movies = entity.Movies;

            return base.Update(persisted);
        }
    }
}
