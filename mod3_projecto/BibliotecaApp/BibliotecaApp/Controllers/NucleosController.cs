using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.Controllers
{
    public class NucleosController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
