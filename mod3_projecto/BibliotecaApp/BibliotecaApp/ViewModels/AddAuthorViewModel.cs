using BibliotecaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.ViewModels
{
    public class AddAuthorViewModel
    {
        public List<Autor> AreAutores { get; set; }
        public List<Autor> NonAutores { get; set; }
        public int ObraId { get; set; }
        public Obra Obra { get; set; }

        public int[] AddAuthors { get; set; }
        public int[] RemoveAuthors { get; set; }
    }
}
