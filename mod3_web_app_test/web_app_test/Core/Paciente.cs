using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class Paciente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        #nullable enable
        public string? Beneficiario { get; set; }
        #nullable disable
    }
}
