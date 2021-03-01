using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibliotecaApp.Models.Data;
using BibliotecaApp.Models;
using Microsoft.AspNetCore.Authorization;
using BibliotecaApp.ViewModels;

namespace BibliotecaApp.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class NucleosController : Controller
    {
        private readonly INucleosRepository _nucleosRepository;
        private readonly IObrasRepository _obrasRepository;

        public NucleosController(INucleosRepository nucleosRepository, IObrasRepository obrasRepository)
        {
            _nucleosRepository = nucleosRepository;
            _obrasRepository = obrasRepository;
        }
        public IActionResult Index()
        {
            return View(_nucleosRepository.GetAllNucleos());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Nucleo nucleo)
        {
            if(ModelState.IsValid)
            {
                _nucleosRepository.CreateNucleo(nucleo);
                return RedirectToAction("Index");
            }
            else
            {
                return View(nucleo);
            }
        }

        public IActionResult Remove(int id)
        {
            _nucleosRepository.RemoveNucleo(id);
            return RedirectToAction("Index");
        }

        public IActionResult Obras(int id)
        {
            var nucleo = _nucleosRepository.GetNucleoById(id);
            var copiasNucleo = new List<NumCopias>();
            var numCopiasDict = _nucleosRepository.GetNumCopiasTodasObras(nucleo);
            foreach (Obra obra in _obrasRepository.GetAllObras())
            {
                int numCopias = 0;
                if (numCopiasDict.ContainsKey(obra.Id))
                    numCopias = numCopiasDict[obra.Id];
                copiasNucleo.Add(new NumCopias { Obra = obra, Num = numCopias});
            }



            var viewModel = new ObrasNucleoViewModel
            {
                Nucleo = nucleo,
                NumCopias = copiasNucleo.OrderByDescending(x => x.Num).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Obras(ObrasNucleoViewModel viewModel)
        {
            var nucleo = _nucleosRepository.GetNucleoById(viewModel.NucleoId);
            var numCopiasDict = _nucleosRepository.GetNumCopiasTodasObras(nucleo);

            for (int i = 0; i < viewModel.Obras.Length; i++)
            {
                int obraId = viewModel.Obras[i];
                int novoNumCopias = viewModel.NovoNumeroCopias[i];
                if (numCopiasDict.ContainsKey(obraId))
                {
                    if(numCopiasDict[obraId] != novoNumCopias)
                    {
                        _nucleosRepository.UpdateNumCopias(nucleo, obraId, novoNumCopias);
                    }
                }
                else if (novoNumCopias != 0)
                {
                    _nucleosRepository.UpdateNumCopias(nucleo, obraId, novoNumCopias);
                }
            }
            return RedirectToAction("Obras");
        }
    }
}
