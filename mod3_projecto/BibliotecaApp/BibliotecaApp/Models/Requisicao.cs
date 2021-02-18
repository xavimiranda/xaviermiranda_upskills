using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.Models
{
    public class Requisicao
    {
        [Key]
        public int Id { get; set; }
        public Leitor Leitor { get; set; }
        public Obra Obra { get; set; }
        public Nucleo Nucleo { get; set; }
        public DateTime DataRequisicao { get; set; }
        public DateTime DataLimite { get; set; }
        public DateTime DataEntregue { get; set; }
    }
}
