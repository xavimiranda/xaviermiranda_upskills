using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace web_app_test.Pages.Alineas
{
    public class Alinea4Model : PageModel
    {
        private readonly HospDB db;

        [BindProperty(SupportsGet = true)]
        public bool ExcluirTaxas { get; set; }

        public List<Paciente> Pacientes { get; set; }
        public List<Consulta> Consultas { get; set; }
        public Alinea4Model(HospDB db)
        {
            this.db = db;            
        }
        public void OnGet()
        {
            Consultas = new List<Consulta>();
            Pacientes = new List<Paciente>();
            var todosPacientes = db.GetPacientes();
            var todasConsultas = db.GetConsultas().Where(c => c.IncluirTaxa == true).ToList();

            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            foreach (var consulta in todasConsultas)
            {
                var paciente = todosPacientes.Find(p => p.Id == consulta.Id_Paciente);
                DateTime aniversario = new DateTime(DateTime.Now.Year, paciente.DataNascimento.Month, paciente.DataNascimento.Day);
                int semanaAniversario = calendar.GetWeekOfYear(aniversario, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                int semanaConsulta = calendar.GetWeekOfYear(consulta.Data_Consulta, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                if (semanaAniversario == semanaConsulta)
                {
                    if(ExcluirTaxas)
                    {
                        consulta.IncluirTaxa = false;
                        db.UpdateConsulta(consulta);
                        TempData["Message"] += $"Exclui-se taxa para consulta a {consulta.Data_Consulta}\n";
                    }
                    else
                    {
                        Consultas.Add(consulta);
                        Pacientes.Add(paciente);
                    }
                }
            }
            
        }
    }
}
