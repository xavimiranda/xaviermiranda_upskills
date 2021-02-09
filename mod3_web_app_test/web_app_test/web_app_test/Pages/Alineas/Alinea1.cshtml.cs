using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace web_app_test.Pages.Alineas
{
    public class Alinea1Model : PageModel
    {
        private readonly HospDB db;

        public Medico MaisVelho { get; set; }
        public Medico MaisNovo { get; set; }

        public Alinea1Model(HospDB db)
        {
            this.db = db;
        }
        public void OnGet()
        {
            var medicos = db.GetMedicos();
            MaisNovo = medicos.Where(m => m.Ativo).OrderByDescending(m => m.DataNascimento).FirstOrDefault();
            MaisVelho = medicos.Where(m => m.Ativo).OrderBy(m => m.DataNascimento).FirstOrDefault();
        }
    }
}
