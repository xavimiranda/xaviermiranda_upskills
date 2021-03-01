using BibliotecaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.ViewModels
{
    public class UserViewModel
    {
        public Leitor Leitor { get; set; }
        public IEnumerable<Requisicao> RequisicoesActivas { get; set; }
        public IEnumerable<Requisicao> RequisicoesEntregues { get; set; }
    }
}
