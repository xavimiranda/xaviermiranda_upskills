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
    public class Alinea3Model : PageModel
    {
        private readonly HospDB db;
        public List<Consulta> Consultas { get; set; }
        public List<Medico> Medicos { get; private set; }
        public List<Paciente> Pacientes { get; set; }
        public List<Especialidade> Especialidades { get; set; }

        public Alinea3Model(HospDB db)
        {
            this.db = db;
            Consultas = db.GetConsultas().Where(c => c.Data_Consulta.Month == DateTime.Now.Month).OrderBy(c => c.Data_Consulta).ToList();
            Medicos = db.GetMedicos();
            Pacientes = db.GetPacientes();
            Especialidades = db.GetEspecialidades();            
        }
        public void OnGet()
        {
        }
    }
}
