using BibliotecaApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.ViewModels
{
    public class ObrasCreateViewModel
    {
        public Obra Obra { get; set; }
        public int ClassificacaoId { get; set; }
        public IEnumerable<SelectListItem> Classificacoes { get; set; }

    }
}
