using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace web_app_test.Pages.Alineas
{
    public class Alinea5Model : PageModel
    {
        private readonly HospDB db;

        [BindProperty]
        public int Ano { get; set; }
        public IEnumerable<DataRow> Hist { get; set; }

        public Alinea5Model(HospDB db)
        {
            this.db = db;
        }
        public void OnGet(int Ano)
        {
            Hist = db.GetDataTable("Hist_Consultas").AsEnumerable();
        }
        public IActionResult OnPost()
        {
            db.GuardarHist(Ano);
            return RedirectToPage("./Alinea5");
        }
    }
}
