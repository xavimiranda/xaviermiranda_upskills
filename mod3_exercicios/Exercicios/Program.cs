using System;

namespace Exercicios
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("\n=================================");
            //Console.WriteLine("\n==========Sum Or Triple==========");
            //SumOrTripleTest();
            //Console.WriteLine("\n==========mult and div===========");
            //MultAndDivTest();
            //Console.WriteLine("\n==========Guessing Game==========");
            //Console.WriteLine(SimpleNumbers.GuessingGame());
            //Console.WriteLine("\n==========Find with while========");
            //SimpleNumbers.FindTargetsWithWhile();
            //Console.WriteLine("\n==========Rectangle==============");
            //var rect = new Rectangle(100, 50);
            //Console.WriteLine(rect);
            //Console.WriteLine("\n=========Numero Inteiro=======");
            
            
        }

        private static void MultAndDivTest(int maxCases = 5)
        { 
            Random rand = new Random();
            for (int i = 0; i < maxCases; i++)
            {
                double num1 = rand.Next(1, 11);
                double num2 = rand.Next(1, 11);
                Tuple<double, double> t = SimpleNumbers.MultAndDiv(num1, num2);
                Console.WriteLine($"MultAndDiv {num1,3},{num2,3} -> { SimpleNumbers.MultAndDiv(num1, num2),6}");
            }
        }

        static void SumOrTripleTest()
        {
            int[,] sotTest = {
                {-2, -2 },
                {2, 2 },
                {3, 2 },
                {-1, 4}
            };

            for (int i = 0; i < sotTest.GetLength(0); i++)
            {
                int num1 = sotTest[i, 0];
                int num2 = sotTest[i, 1];
                Console.WriteLine($"SumOrTriple {num1,3},{num2,3} -> { SimpleNumbers.SumOrTriple(num1, num2),6}");

            }
        }

        static void FindTargetsTest()
        {
            
        }
    }
}
