using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.Models
{
    public class Nucleo
    { 
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Morada { get; set; }

        public List<Obra> Obras { get; set; } = new List<Obra>();
    }
}
