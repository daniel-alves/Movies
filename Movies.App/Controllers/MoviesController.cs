using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.Domain;
using Movies.Domain.Entities;
using Movies.Infra.Services.Common;

namespace Movies.App.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieCrudService<Movie> _movieService;

        private readonly IMovieCrudService<Genre> _genreService;
        
        public MoviesController(IMovieCrudService<Movie> movieService, IMovieCrudService<Genre> genreService)
        {
            _genreService = genreService;
            _movieService = movieService;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await _movieService.GetAll().Include(e => e.Genre).ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(long id)
        {
            var movie = await _movieService.GetAll()
                .Include(e => e.Genre)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (movie == null) return NotFound();

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewBag.Genres = GetGenresList();

            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Active,GenreId,Id")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                movie.CreatedAt = DateTime.Now;

                await _movieService.Insert(movie);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Genres = GetGenresList(movie.GenreId);

            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(long id)
        {

            var movie = await _movieService.GetAll()
                .Include(e => e.Genre)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (movie == null) return NotFound();

            ViewBag.Genres = GetGenresList(movie.GenreId);

            return View(movie);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Movie movie)
        {
            if (id != movie.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try{
                    var entity = await _movieService.GetByIdAsync(id);

                    entity.Name = movie.Name;
                    entity.GenreId = movie.GenreId;
                    entity.Active = movie.Active;

                    await _movieService.Update(entity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id)) NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Genres = GetGenresList(movie.GenreId);

            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(long id)
        {
            var movie = await _movieService.GetByIdAsync(id);

            if (movie == null) return NotFound();

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _movieService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        // GET: Genres/SelectList
        [HttpGet, ActionName("SelectList")]
        public async Task<IActionResult> SelectList(string term)
        {
            var list = await _movieService.GetAll()
                .Where(e => e.Name.Contains(term) && e.Active)
                .ToListAsync();

            return Json(list.Select(e => new { id = e.Id, text = e.Name }));
        }

        private bool MovieExists(long id)
        {
            return _movieService.Exists(id);
        }

        private SelectList GetGenresList(long? selectedId = null)
        {
            return new SelectList(_genreService.GetAll().Where(e => e.Active).ToList(), "Id", "Name", selectedId);
        }
    }
}
