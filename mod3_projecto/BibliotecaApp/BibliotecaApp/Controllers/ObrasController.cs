using BibliotecaApp.Models;
using BibliotecaApp.Models.Data;
using BibliotecaApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.Controllers
{
    
    public class ObrasController : Controller
    {
        private readonly IObrasRepository _obrasRepository;
        private readonly IClassificacaoRepository _classificacaoRepository;
        private readonly IAutoresRepository _autoresRepository;

        public ObrasController(IObrasRepository obrasRepository, 
            IClassificacaoRepository classificacaoRepository,
            IAutoresRepository autoresRepository)
        {
            _obrasRepository = obrasRepository;
            _classificacaoRepository = classificacaoRepository;
            _autoresRepository = autoresRepository;
        }
        
        public IActionResult Index()
        {
            
            var obrasViewModel = new ObrasViewModel { Obras = _obrasRepository.GetAllObras() };
            return View(obrasViewModel);
        }    

        [HttpPost]
        public IActionResult Search(string? SearchTitulo, string? SearchAutor, int? SearchAno)
        {
            var titulo = SearchTitulo != null ? SearchTitulo.Trim().ToLower() : null;
            var autor = SearchAutor != null ? SearchAutor.Trim().ToLower() : null;
            
            var obrasViewModel = new ObrasViewModel { Obras = _obrasRepository.Search(titulo, autor, SearchAno) };
            return View("Index", obrasViewModel);
        }

        [Authorize(Roles = "SuperAdmin, Admin")]
        public IActionResult Create()
        {
            var viewModel = new ObrasCreateViewModel();
            var classificacoes = _classificacaoRepository.GetAllClassificacoes().Select(c => new SelectListItem { Text = c.Nome, Value = c.Id.ToString() });
            var autores = _autoresRepository.GetAllAutores().Select(a => new SelectListItem { Text = a.Nome, Value = a.Id.ToString() });
            viewModel.Classificacoes = classificacoes;
            viewModel.Obra = new Obra();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(ObrasCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var obra = viewModel.Obra;
                obra.AdicionadoEm = DateTime.UtcNow;
                obra.Classificacao = _classificacaoRepository.GetClassificacaoById(viewModel.ClassificacaoId);
                int newId = _obrasRepository.CreateObra(viewModel.Obra);
                return RedirectToAction("Details", new { id = newId});
            }
            else
                return View(viewModel);
        }

        public IActionResult Details(int id)
        {
            var obra = _obrasRepository.GetObraById(id);
            if (obra != null)
                return View(obra);
            else
                return RedirectToPage("Error");
        }

        //public IActionResult AddAuthor(int id)
        //{

        //}
    }
}
