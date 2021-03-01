using BibliotecaApp.Models;
using BibliotecaApp.Models.Data;
using BibliotecaApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.Controllers
{
    [Authorize]
    public class ObrasController : Controller
    {
        private readonly IObrasRepository _obrasRepository;
        private readonly IClassificacaoRepository _classificacaoRepository;
        private readonly IAutoresRepository _autoresRepository;
        private readonly INucleosRepository _nucleosRepository;

        public ObrasController(IObrasRepository obrasRepository, 
            IClassificacaoRepository classificacaoRepository,
            IAutoresRepository autoresRepository,
            INucleosRepository nucleosRepository)
        {
            _obrasRepository = obrasRepository;
            _classificacaoRepository = classificacaoRepository;
            _autoresRepository = autoresRepository;
            _nucleosRepository = nucleosRepository;
        }

        public IActionResult Index()
        {
            
            string? nucleoSessionString = HttpContext.Session.GetString("nucleo");
            int nucleoId;
            List<Obra> obras;
            var numCopias = new Dictionary<int, int>();
            if (nucleoSessionString != null)
            {
                nucleoId = int.Parse(nucleoSessionString);

                var nucleo = _nucleosRepository.GetNucleoById(nucleoId);

                obras = _obrasRepository.GetAllObrasFromNucleo(nucleoId).ToList();
                foreach (var obra in obras)
                {
                    numCopias.Add(obra.Id, _nucleosRepository.GetNumCopiasObra(nucleo, obra.Id));
                }
            }
            else
            {
                obras = _obrasRepository.GetAllObras().ToList(); 
                foreach (var obra in obras)
                {
                    numCopias.Add(obra.Id, 0);
                }
            }


            


            var obrasViewModel = new ObrasViewModel { Obras = obras, NumCopias = numCopias };
            return View(obrasViewModel);
        }    

        [HttpPost]
        public IActionResult Search(string? SearchTitulo, string? SearchAutor, int? SearchAno)
        {
            var titulo = SearchTitulo != null ? SearchTitulo.Trim().ToLower() : null;
            var autor = SearchAutor != null ? SearchAutor.Trim().ToLower() : null;
            int nucleoId = int.Parse(HttpContext.Session.GetString("nucleo"));
            Nucleo nucleo = _nucleosRepository.GetNucleoById(nucleoId);

            var obras = _obrasRepository.SearchInNucleo(nucleoId, titulo, autor, SearchAno);
            var numCopias = new Dictionary<int, int>();
            foreach (var obra in obras)
            {
                numCopias.Add(obra.Id, _nucleosRepository.GetNumCopiasObra(nucleo, obra.Id));
            }
            var obrasViewModel = new ObrasViewModel { Obras = obras, NumCopias = numCopias };
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

        public IActionResult AddAutor(int id)
        {
            var areAutores = new List<Autor>();
            var notAutores = new List<Autor>();
            var obra = _obrasRepository.GetObraById(id);
            foreach(var autor in _autoresRepository.GetAllAutoresWithObras())
            {
                if (_autoresRepository.IsAuthorOf(autor, id))
                {
                    areAutores.Add(autor);
                }
                else
                {
                    notAutores.Add(autor);
                }
            }
            var viewModel = new AddAuthorViewModel
            {
                ObraId = id,
                Obra = obra,
                AreAutores = areAutores,
                NonAutores = notAutores
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddAutor(AddAuthorViewModel viewModel)
        {
            var obra = _obrasRepository.GetObraById(viewModel.ObraId);
            foreach(var authorId in viewModel.AddAuthors ?? new int[] { })
            {
                var autor = _autoresRepository.GetAutorByIdWithObras(authorId);
                _autoresRepository.AddObra(autor, obra.Id);
            }
            foreach (var authorId in viewModel.RemoveAuthors ?? new int[] { })
            {
                var autor = _autoresRepository.GetAutorByIdWithObras(authorId);
                _autoresRepository.RemoveObra( autor, obra.Id);
            }
            return RedirectToAction("Details", new { id =  obra.Id});
        }

        public IActionResult CreateAutor(string redirectUrl) 
        {
            
            return View(new CreateAutorViewModel 
            {
                RedirectUrl = redirectUrl
            });
        }

        [HttpPost]
        public IActionResult CreateAutor(CreateAutorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var autor = new Autor
                {
                    Nome = viewModel.Nome,
                    Nascimento = viewModel.Nascimento,
                    Obito = viewModel.Obito
                };

                _autoresRepository.CreateAutor(autor);
                
                return Redirect(viewModel.RedirectUrl);
            }
            else
            {
                return View(viewModel);
            }
        }
    }
}
