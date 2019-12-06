using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.Domain;
using Movies.Domain.Entities;
using Movies.Infra.Repositories.Common;

namespace Movies.App.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieRepository<Genre> _genreRepository;

        private readonly IMovieRepository<Movie> _movieRepository;

        private readonly IMapper _mapper;

        public MoviesController(IMapper mapper, IMovieRepository<Movie> movieRepository, IMovieRepository<Genre> genreRepository)
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await _movieRepository.GetAll().Include(e => e.Genre).ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(long id)
        {
            var movie = await _movieRepository.GetAll()
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
                await _movieRepository.AddAsync(movie);
                await _movieRepository.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(long id)
        {

            var movie = await _movieRepository.GetAll()
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
                    var entity = await _movieRepository.GetByIdAsync(id);

                    entity.Name = movie.Name;
                    entity.GenreId = movie.GenreId;
                    entity.Active = movie.Active;

                    _movieRepository.Update(entity);
                    await _movieRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id)) NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(long id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);

            if (movie == null) return NotFound();

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            _movieRepository.Remove(id);
            await _movieRepository.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(long id)
        {
            return _movieRepository.Exists(id);
        }

        private SelectList GetGenresList(long? selectedId = null)
        {
            return new SelectList(_genreRepository.GetAll().Where(e => e.Active).ToList(), "Id", "Name", selectedId);
        }
    }
}
