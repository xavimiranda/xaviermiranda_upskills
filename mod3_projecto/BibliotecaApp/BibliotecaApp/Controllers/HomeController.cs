using BibliotecaApp.Models;
using BibliotecaApp.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly INucleosRepository _nucleosRepository;

        public HomeController(INucleosRepository nucleosRepository)
        {
            _nucleosRepository = nucleosRepository;
        }

        
        public IActionResult Index()
        {
            IEnumerable<SelectListItem> nucleos = _nucleosRepository.GetAllNucleos().Select(n => new SelectListItem { Text = n.Nome, Value = n.Id.ToString() });
            return View(nucleos);
        }

        [HttpPost]
        public IActionResult Index(string nucleo)
        {
            HttpContext.Session.SetString("nucleo", nucleo);
            return RedirectToAction("Index", "Obras");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
