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
        
        public GenreController(IMapper mapper, IGenreCrudService service) 
            : base(mapper, service)
        {
            
        }

        [HttpGet, ActionName("SelectList")]
        public async Task<IActionResult> SelectList(string term)
        {
            var list = await _service.GetAll()
                .Where(e => (e.Name.Contains(term) || string.IsNullOrWhiteSpace(term)) && e.Active)
                .ToListAsync();

            return Json(list.Select(e => new { id = e.Id, text = e.Name }));
        }
    }
}
