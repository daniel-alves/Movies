using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.App.Models.Genres;
using Movies.Domain;
using Movies.Framework.Controllers;
using Movies.Infra.Services.Genres;

namespace Movies.App.Controllers
{
    [Authorize]
    public class GenreController : CrudController<Genre, GenreViewModel>
    {
        private readonly IGenreCrudService _service;

        public GenreController(IMapper mapper, IGenreCrudService service) 
            : base(mapper, service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult SelectList(string term)
        {
            var list = _service.GetAllActiveAndContainName(term)
                .Select(e => new { id = e.Id, text = e.Name });

            return Json(list);
        }
    }
}
