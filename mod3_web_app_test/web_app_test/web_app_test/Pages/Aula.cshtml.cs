using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace web_app_test.Pages
{
    public class AulaModel : PageModel
    {
        public List<Medico> Medicos { get; set; }
        public string Cumprimento { get; set; }
        public void OnGet()
        {
            Cumprimento = "Olá Mundo!";

        }
    }
}
