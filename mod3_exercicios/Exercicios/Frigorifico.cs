using System;
using System.Collections.Generic;
using System.Text;

namespace Exercicios
{
    public class Frigorifico : Electrodomestico
    {
        public Frigorifico(string marca, string modelo, bool combinado = true) : base(marca, modelo)
        {
            Combinado = combinado;
            TempFrig = 8d;
            if (Combinado)
                TempCong = -2d;
        }

        public static double MaxFrigTemp = 15d;
        public static double MinFrigTemp = 0d;
        public static double MaxCongTemp = 0d;
        public static double MinCongTemp = -10d;

        private double _tempFrig;
        public double TempFrig
        {
            get => _tempFrig;
            set
            {
                if (value >= MinFrigTemp && value <= MaxFrigTemp)
                    _tempFrig = value;
                else
                    throw new ArgumentOutOfRangeException($"O Frigorífico só aceita temperaturas entre os {MinFrigTemp} e os {MaxFrigTemp}º");
            }
        }
        
        private double? _tempCong;

        public double? TempCong
        {
            get => Combinado ? _tempCong : null;
            set 
            {
                if (Combinado)
                {
                    if (value >= MinCongTemp && value <= MaxFrigTemp)
                        _tempCong = value;
                    else
                        throw new ArgumentOutOfRangeException($"O congelador só aceita temperaturas entre os {MinCongTemp} e s {MaxCongTemp}");
                }
                else
                {
                    throw new ArgumentException("Este modelo não tem congelador");
                }
            }
        }
        
        public bool Combinado { get; private set; }
        public bool PortaAbertaFrig { get; set; }
        
        private bool? _portaAbertaCong;
        public bool? PortaAbertaCong 
        {
            get => Combinado ? _portaAbertaCong : null; 
            set
            {
                _portaAbertaCong = Combinado ? value : throw new ArgumentException("Este modelo não tem congelador.");
            }
        }

    }
}
