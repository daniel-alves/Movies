using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Movies.App.Models.Locations;
using Movies.Domain.Entities;
using Movies.Framework.Controllers;
using Movies.Infra.Services.Locations;

namespace Movies.App.Controllers
{
    [Authorize]
    public class LocationController : CrudController<Location, LocationViewModel>
    {
        
        public LocationController(IMapper mapper, ILocationCrudService service) : base(mapper, service)
        {
        }
    }
}
