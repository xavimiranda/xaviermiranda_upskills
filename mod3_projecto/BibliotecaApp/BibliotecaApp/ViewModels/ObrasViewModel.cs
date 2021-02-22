using BibliotecaApp.Models;
using BibliotecaApp.Models.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.ViewModels
{
    public class ObrasViewModel
    {
        public IEnumerable<Obra> Obras { get; set; }
        public string? SearchTitulo { get; set; }
        public string? SearchAutor { get; set; }
        public int? SearchAno { get; set; }
    }
}
