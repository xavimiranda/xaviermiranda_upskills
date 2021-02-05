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
    public class ListModel : PageModel
    {
        public List<Medico> Medicos { get; set; }
        public void OnGet()
        {
            HospDB db = new HospDB();
            Medicos = db.GetMedicos();
        }
    }
}
