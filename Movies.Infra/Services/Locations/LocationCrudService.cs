using Movies.Domain.Entities;
using Movies.Infra.Repositories.Common;
using Movies.Infra.Services.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Infra.Services.Locations
{
    public class LocationCrudService : CommonCrudService<Location>, ILocationCrudService
    {
        public LocationCrudService(ICommonRepository<Location> repository) 
            : base(repository)
        {

        }
    }
}
