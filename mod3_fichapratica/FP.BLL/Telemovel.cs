using System;
using System.Collections.Generic;
using System.Text;

namespace FP.BLL
{
    public class Telemovel : Telefone
    {

        public new AtributosTelemovel Atributos { get; set; } = new AtributosTelemovel();

        public Telemovel(AtributosTelemovel a) : base(a)
        {
            Atributos = a;
        }
    }

}
