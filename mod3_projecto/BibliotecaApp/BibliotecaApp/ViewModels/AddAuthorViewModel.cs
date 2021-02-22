using BibliotecaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.ViewModels
{
    public class AddAuthorViewModel
    {
        public IEnumerable<Autor> Autores { get; set; }
        public int ObraId { get; set; }
    }
}
