using System;
using System.Collections.Generic;
using System.Text;

namespace Exercicios
{
    class NumeroInteiro
    {
        public NumeroInteiro( int num)
        {
            Numero = num;
        }

        public int Numero { get; private set; }
        public bool EPar() => Numero % 2 == 0;
        public bool EPositivo() => Numero >= 0;
        public bool EPrimo()
        {
            if (Numero == 0 || Numero == 1)
                return false;
            for(int i = 2; i < Math.Sqrt(Numero); i++)
            {
                if (Numero % i == 0) 
                    return false;
            }
            return true;
        }
        public string Ordem()
        {
            string result = string.Empty;
            if (Numero >= 10 && Numero < 100)
                result = "dezenas";
            else if (Numero < 1000)
                result = "centenas";
            else if (Numero < 10000)
                result = "milhares";
            else
                result = "indeterminada";

            return result;
        }   
    }
}
