using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibliotecaApp.Models.Data;
using BibliotecaApp.Models;

namespace BibliotecaApp.Controllers
{
    public class NucleosController : Controller
    {
        private readonly INucleosRepository _nucleosRepository;

        public NucleosController(INucleosRepository nucleosRepository)
        {
            _nucleosRepository = nucleosRepository;
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
    }
}
