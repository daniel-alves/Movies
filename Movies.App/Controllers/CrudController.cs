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
        
        //página de listagem e paginação
        public virtual IActionResult Index(int page = 1)
        {
            var pageViewModel = new PageViewModel<TViewModel>() { Page = page };

            var list = _service.GetPage(pageViewModel.Limit, pageViewModel.Offset);

            pageViewModel.List = _mapper.Map<List<TViewModel>>(list);

            return View(pageViewModel);
        }

        //página de detalhes da entidade
        public virtual async Task<IActionResult> Details(long id)
        {
            var entity = await _service.GetByIdAsync(id);

            return View(_mapper.Map<TViewModel>(entity));
        }

        //página com formulário para cadastro
        public virtual IActionResult Create()
        {
            return View();
        }
        
        //ação para persistir os dados da tela no banco de dados
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
        
        //página com formulário para edição
        public virtual async Task<IActionResult> Edit(long id)
        {
            var entity = await _service.GetByIdAsync(id);

            return View(_mapper.Map<TViewModel>(entity));
        }
        
        //ação para persistir os dados alterados na base
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
        
        //pagina para confirmação da exclusão sómente se a entity não tiver referências
        public virtual async Task<IActionResult> Delete(long id)
        {
            var entity = await _service.GetByIdAsync(id);

            var viewModel = _mapper.Map<TViewModel>(entity);
            viewModel.CanDelete = _service.CanDelete(id);

            return View(viewModel);
        }

        //pagina para confirmação da exclusão em massa sómente se a entity não tiver referência
        [HttpPost]
        public virtual IActionResult DeleteMany(long[] ids)
        {
            if (!ids.Any()) return RedirectToAction(nameof(Index));

            var viewModels = _mapper.Map<List<TViewModel>>(_service.GetAllById(ids));

            viewModels.ForEach(v => v.CanDelete = _service.CanDelete(v.Id));

            return View(viewModels);
        }

        //ação de exclusão em massa das entities que não tem referência
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

        //ação de excçusão de uma entity sómente se a entity não tiver referências
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

        //verifica pelo id se o objeto existe
        private bool GenreExists(long id)
            => _service.Exists(id);
    }
}