using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.App.Models.Movies;
using Movies.Domain.Entities;
using Movies.Framework.Controllers;
using Movies.Infra.Services.Movies;

namespace Movies.App.Controllers
{
    [Authorize]
    public class MovieController : CrudController<Movie, MovieViewModel>
    {
        private readonly IMovieCrudService _movieService;

        public MovieController(IMapper mapper, IMovieCrudService service) 
            : base(mapper, service)
        {
            _movieService = service;
        }

        //lista todos os filmes ativos e filtra sómente os filmes que contenham o valor de term no nome
        [HttpGet]
        public async Task<IActionResult> SelectList(string term)
        {
            var list = _movieService.GetAllActiveAndContainName(term)
                .Select(e => new { id = e.Id, text = e.Name });

            return Json(list);
        }
    }
}
