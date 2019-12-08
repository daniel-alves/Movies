using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.App.Models.Movies;
using Movies.Domain.Entities;
using Movies.Framework.Controllers;
using Movies.Infra.Services.Movies;

namespace Movies.App.Controllers
{
    [Authorize]
    public class MovieController : CrudController<Movie, MovieViewModel>
    {
        public MovieController(IMapper mapper, IMovieCrudService service) 
            : base(mapper, service)
        {
        }

        // GET: Genres/SelectList
        [HttpGet, ActionName("SelectList")]
        public async Task<IActionResult> SelectList(string term)
        {
            var list = await _service.GetAll()
                .Where(e => e.Name.Contains(term) && e.Active)
                .ToListAsync();

            return Json(list.Select(e => new { id = e.Id, text = e.Name }));
        }
    }
}
