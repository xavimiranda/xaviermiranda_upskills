using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class Consulta
    {
        public int Id_Medico { get; set; }
        public int Id_Paciente { get; set; }
        public int Id_Especialidade { get; set; }
        public DateTime Data_Consulta { get; set; }
        public bool IncluirTaxa { get; set; }
    }
}
