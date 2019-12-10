using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        //lista todos os genêros ativos, e caso term não seja vazio filtra por genêros que tenham o valor de term no nome
        [HttpGet]
        public IActionResult SelectList(string term)
        {
            var list = _service.GetAllActiveAndContainName(term)
                .Select(e => new { id = e.Id, text = e.Name });

            return Json(list);
        }
    }
}
