using System;
using System.Collections.Generic;
using System.Text;

namespace FP.BLL
{
    public class Telefone
    {
        private bool _ligado;

        public bool Ligado
        {
            get => _ligado;
            set
            {
                if (_nivelBateria > 0)
                    _ligado = !_ligado;

                if (_ligado)
                    TempoLigado++;
            }
        }

        private int _nivelBateria;

        public int NivelBateria
        {
            get => _nivelBateria;
            set
            {
                if (_ligado)
                    _nivelBateria = _nivelBateria / TempoLigado;
                else
                    _nivelBateria = _nivelBateria / TempoLigado / 3;

                if (_nivelBateria == 0)
                    _ligado = false;
            }
        }

        public int SinalRede { get; private set; }
        public bool Bloqueado { get; private set; }
        public int TempoLigado { get; private set; }
        public AtributosTelefone Atributos { get; private set; } = new AtributosTelefone();

        public Telefone(AtributosTelefone atributos)
        {
            Atributos = atributos;
        }
    }
}
