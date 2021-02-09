using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace web_app_test.Pages.Alineas
{
    public class Alinea2Model : PageModel
    {
        private readonly HospDB db;
        public int[] ConsultasPorMes { get; set; } = new int[12];
        [BindProperty]
        public int MedicoId { get; set; }
        public IEnumerable<SelectListItem> MedicosAtivos { get; set; }
        
        public string[] Meses = new string[]
        {
            "Janeiro",
            "Fevereiro",
            "Março",
            "Abril",
            "Maio",
            "Junho",
            "Julho",
            "Agosto",
            "Setembro",
            "Outubro",
            "Novembro",
            "Dezembro"
        };
        public Alinea2Model(HospDB db)
        {
            this.db = db;
            MedicosAtivos = db.GetMedicos().Where(m => m.Ativo).Select(m => new SelectListItem() { Text = m.Nome, Value = (m.Id).ToString() });
        }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            var medico = db.GetMedicoById(MedicoId);
            if (medico == null)
                return RedirectToPage("./Alineas/Alinea2");

            var consultas = db.GetConsultas().Where(c => (c.Id_Medico == medico.Id) && c.Data_Consulta.Year == DateTime.Now.Year).OrderBy(c => c.Data_Consulta);

            foreach (var consulta in consultas)
            {
                ConsultasPorMes[consulta.Data_Consulta.Month-1]++;
            }

            return Page();
        }
    }
}

