using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities;
using Movies.Infra.Services.Common;

namespace Movies.App.Controllers
{
    public class LocationsController : Controller
    {
        private readonly IMovieCrudService<Movie> _movieService;

        private readonly IMovieCrudService<Location> _locationService;
        
        public LocationsController(IMovieCrudService<Movie> movieService, IMovieCrudService<Location> locationService)
        {
            _movieService = movieService;
            _locationService = locationService;
        }

        // GET: Locations
        public async Task<IActionResult> Index()
        {
            return View(await _locationService.GetAll().ToListAsync());
        }

        // GET: Locations/Details/5
        public async Task<IActionResult> Details(long id)
        {
            var location = await _locationService.GetByIdAsync(id);

            if (location == null) return NotFound();

            return View(location);
        }

        // GET: Locations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cpf,LocatedAt,Id")] Location location)
        {
            if (ModelState.IsValid)
            {
                location.LocatedAt = DateTime.Now;

                await _locationService.Insert(location);
                
                return RedirectToAction(nameof(Index));
            }

            return View(location);
        }

        // GET: Locations/Edit/5
        public async Task<IActionResult> Edit(long id)
        {
            var location = await _locationService.GetByIdAsync(id);

            if (location == null) return NotFound();

            return View(location);
        }

        // POST: Locations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Cpf,LocatedAt,Id")] Location location)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = await _locationService.GetByIdAsync(id);
                    
                    entity.Cpf = location.Cpf;

                    await _locationService.Update(entity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.Id)) return NotFound();
                    else throw; 
                }

                return RedirectToAction(nameof(Index));
            }

            return View(location);
        }

        // GET: Locations/Delete/5
        public async Task<IActionResult> Delete(long id)
        {
            var location = await _locationService.GetByIdAsync(id);

            if (location == null) return NotFound();

            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _locationService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(long id)
        {
            return _locationService.Exists(id);
        }
    }
}
