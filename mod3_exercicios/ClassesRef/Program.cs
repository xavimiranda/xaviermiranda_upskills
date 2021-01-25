using System;

namespace ClassesRef
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new Atributos() { marca = "Samsung", modelo = "A4", altura = 1, largura = 2, profundidade = 3 };
            var f = new Frigorifico(a);
            Console.WriteLine(f.atrib.marca);
            a.marca = "Huawei";
            Console.WriteLine(f.atrib.marca);
            a = null;
            Console.WriteLine(f.atrib.marca);

        }

    }

    public class Electro
    {
        public virtual void Ligar() { Console.WriteLine("LIGADO"); }
    }
    public class Frigorifico : Electro
    {
        public Frigorifico(Atributos a)
        {
            atrib = a;
            Temp = 2d;
        }
        public override void Ligar() { Console.WriteLine("F LIGADO"); }
        public static double MinTemp = -2d;
        public static double MaxTemp = 10d;
        public Atributos atrib = new Atributos();
        private double _temp;
        public double Temp
        {
            get { return _temp; }
            set 
            {
                if (value >= MinTemp && value <= MaxTemp)
                    _temp = value;
                else
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public class Atributos
    {
        public string marca;
        public string modelo;
        public int altura;
        public int largura;
        public int profundidade;
    }
}
