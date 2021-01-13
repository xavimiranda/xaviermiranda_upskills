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
    }
}
