using BibliotecaApp.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.Components
{
    public class NucleoInfo : ViewComponent
    {
        private readonly INucleosRepository _nucleosRepository;

        public NucleoInfo(INucleosRepository nucleosRepository)
        {
            _nucleosRepository = nucleosRepository;
        }

        public IViewComponentResult Invoke()
        {
            int nucleoId = int.Parse(HttpContext.Session.GetString("nucleo"));
            var nucleo = _nucleosRepository.GetNucleoById(nucleoId);
            return View(nucleo);
        }
    }
}
