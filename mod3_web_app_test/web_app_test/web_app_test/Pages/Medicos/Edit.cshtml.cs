using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace web_app_test.Pages.Medicos
{
    public class EditModel : PageModel
    {
        private readonly HospDB db;

        [BindProperty]
        public Medico Medico { get; set; }
        public EditModel(HospDB db)
        {
            this.db = db;
        }
        public void OnGet(int? medicoId)
        {
            
            if (medicoId.HasValue)
                Medico = db.GetMedicoById(medicoId.Value);
            else
                Medico = new Medico();

        }

        public IActionResult OnPost()
        {
          

            if (Medico.Id != 0)
            {
                db.UpdateMedico(Medico);
                TempData["PostMessage"] = $"Dr. {Medico.Nome} actualizado.";
            }
            else
            {
                db.AddMedico(Medico);
                TempData["PostMessage"] = $"Dr. {Medico.Nome} adicionado.";
            }

            return RedirectToPage("./List");
        }
    }
}
