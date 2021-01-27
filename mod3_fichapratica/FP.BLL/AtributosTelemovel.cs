using System;

namespace FP.BLL
{
    public class AtributosTelemovel : AtributosTelefone
    {
        public double Ecran { get; set; }
        public int Disco { get; set; }
        public string Processador { get; set; }
        public string So { get; set; }
        public bool Touch { get; set; }
    }
}
