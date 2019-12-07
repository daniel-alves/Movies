using AutoMapper;
using Movies.App.Models.Locations;
using Movies.Domain.Entities;
using Movies.Framework.Controllers;
using Movies.Infra.Services.Locations;

namespace Movies.App.Controllers
{
    public class LocationsController : CrudController<Location, LocationViewModel>
    {
        
        public LocationsController(IMapper mapper, ILocationCrudService service) : base(mapper, service)
        {
        }
    }
}
