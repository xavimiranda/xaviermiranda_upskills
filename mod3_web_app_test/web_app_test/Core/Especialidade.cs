using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class Especialidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public int TaxaPercentual { get; set; }
    }
}
