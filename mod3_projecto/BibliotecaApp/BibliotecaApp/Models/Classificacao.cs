using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.Models
{
    public class Classificacao
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int CDD { get; set; }

        public List<Obra> Obras { get; set; } = new List<Obra>();
    }
}
