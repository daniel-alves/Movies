using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Movies.App.Models.Shared;
using Movies.Framework.Entities;
using Movies.Framework.Services;
using System.Linq;

namespace Movies.Framework.Controllers
{

    //controller genérico deve ser herdado nunca instanciado diretamente por isso foi colocado o abstract
    //possui todas as operações necessárias para um crud básico
    //optei por usar ViewModel pq as vezes é necessário passar valores para a view as quais não pertencem ao domain
    public abstract class CrudController<TEntity, TViewModel> : Controller
        where TEntity : Entity
        where TViewModel : ViewModel
    { 
        protected readonly IMapper _mapper;

        protected readonly ICrudService<TEntity> _service;
        
        public CrudController(IMapper mapper, ICrudService<TEntity> service)
        {
            _mapper = mapper;
            _service = service;
        }
        
        public virtual IActionResult Index(int page = 1)
        {
            var pageViewModel = new PageViewModel<TViewModel>() { Page = page };

            var list = _service.GetPage(pageViewModel.Limit, pageViewModel.Offset);

            pageViewModel.List = _mapper.Map<List<TViewModel>>(list);

            return View(pageViewModel);
        }
        
        public virtual async Task<IActionResult> Details(long id)
        {
            var entity = await _service.GetByIdAsync(id);

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

            return View(_mapper.Map<TViewModel>(entity));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual IActionResult Edit(long id, TViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _service.Update(_mapper.Map<TEntity>(viewModel));
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

            var viewModel = _mapper.Map<TViewModel>(entity);
            viewModel.CanDelete = _service.CanDelete(id);

            return View(viewModel);
        }

        [HttpPost]
        public virtual IActionResult DeleteMany(long[] ids)
        {
            if (!ids.Any()) return RedirectToAction(nameof(Index));

            var viewModels = _mapper.Map<List<TViewModel>>(_service.GetAllById(ids));

            viewModels.ForEach(v => v.CanDelete = _service.CanDelete(v.Id));

            return View(viewModels);
        }

        [HttpPost]
        public virtual IActionResult DeleteManyConfirmed(long[] ids)
        {
            foreach(var id in ids)
            {
                if (_service.CanDelete(id))
                {
                    _service.Delete(id);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual IActionResult DeleteConfirmed(long id)
        {
            if (_service.CanDelete(id))
            {
                _service.Delete(id);

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Delete), new { id });
        }

        private bool GenreExists(long id)
            => _service.Exists(id);
    }
}