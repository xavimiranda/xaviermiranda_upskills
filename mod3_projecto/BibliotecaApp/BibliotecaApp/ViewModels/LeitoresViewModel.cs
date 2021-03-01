using BibliotecaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.ViewModels
{
    public class LeitoresViewModel
    {
        public IEnumerable<Leitor> Leitores { get; set; }
        public Dictionary<string, int> RequisicoesActivas { get; set; }
    }
}
