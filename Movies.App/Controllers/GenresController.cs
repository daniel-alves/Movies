using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Domain;
using Movies.Infra.Services.Common;

namespace Movies.App.Controllers
{
    public class GenresController : Controller
    {
        private readonly IMovieCrudService<Genre> _genreService;

        public GenresController(IMovieCrudService<Genre> service)
        {
            _genreService = service;
        }

        // GET: Genres
        public async Task<IActionResult> Index()
        {
            return View(await _genreService.GetAll().ToListAsync());
        }

        // GET: Genres/Details/5
        public async Task<IActionResult> Details(long id)
        {   
            var genre = await _genreService.GetByIdAsync(id);

            if (genre == null) return NotFound();

            return View(genre);
        }

        // GET: Genres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Active,Id")]Genre genre)
        {
            if (ModelState.IsValid)
            {
                genre.CreatedAt = DateTime.Now;

                await _genreService.Insert(genre);

                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // GET: Genres/Edit/5
        public async Task<IActionResult> Edit(long id)
        {
            var genre = await _genreService.GetByIdAsync(id);

            if (genre == null) return NotFound();

            return View(genre);
        }

        // POST: Genres/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Genre genre)
        {
            if (id != genre.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var entity = await _genreService.GetByIdAsync(id);

                    entity.Name = genre.Name;
                    entity.Active = genre.Active;

                    await _genreService.Update(entity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenreExists(genre.Id)) return NotFound();
                    else throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(genre);
        }

        // GET: Genres/Delete/5
        public async Task<IActionResult> Delete(long id)
        {
            var genre = await _genreService.GetByIdAsync(id);

            if (genre == null) return NotFound();

            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _genreService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private bool GenreExists(long id)
            => _genreService.Exists(id);
    }
}
