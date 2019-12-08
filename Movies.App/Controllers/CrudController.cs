using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.App.Models;
using Movies.Framework.Entities;
using Movies.Framework.Services;
using Movies.Infra.Data.Contexts;

namespace Movies.Framework.Controllers
{
    public abstract class CrudController<TEntity, TViewModel> : Controller
        where TEntity : Entity
        where TViewModel : ViewModel
    { 
        protected readonly IMapper _mapper;

        protected readonly ICrudService<TEntity, MovieContext> _service;

        public CrudController(IMapper mapper, ICrudService<TEntity, MovieContext> service)
        {
            _mapper = mapper;
            _service = service;
        }
        
        public virtual async Task<IActionResult> Index()
        {
            var entity = await _service.GetAll().ToListAsync();

            return View(_mapper.Map<IEnumerable<TViewModel>>(entity));
        }
        
        public virtual async Task<IActionResult> Details(long id)
        {
            var entity = await _service.GetByIdAsync(id);

            if (entity == null) return NotFound();

            return View(_mapper.Map<TViewModel>(entity));
        }
        
        public virtual IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Create(TViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _service.Insert(_mapper.Map<TEntity>(viewModel));

                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }
        
        public virtual async Task<IActionResult> Edit(long id)
        {
            var entity = await _service.GetByIdAsync(id);

            if (entity == null) return NotFound();

            return View(_mapper.Map<TViewModel>(entity));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Edit(long id, TViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.Update(_mapper.Map<TEntity>(viewModel));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenreExists(viewModel.Id)) return NotFound();
                    else throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }
        
        public virtual async Task<IActionResult> Delete(long id)
        {
            var entity = await _service.GetByIdAsync(id);

            if (entity == null) return NotFound();

            var viewModel = _mapper.Map<TViewModel>(entity);
            viewModel.CanDelete = _service.CanDelete(id);

            return View(viewModel);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_service.CanDelete(id))
            {
                await _service.Delete(id);

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Delete));
        }

        private bool GenreExists(long id)
            => _service.Exists(id);
    }
}