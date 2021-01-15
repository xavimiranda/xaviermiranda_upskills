using System;
using System.Linq;

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
            //SimpleNumbers.GuessingGame();
            //Console.WriteLine("\n==========Find with while========");
            //SimpleNumbers.FindTargetsWithWhile();
            //Console.WriteLine("\n==========Rectangle==============");
            //var rect = new Rectangle(100, 50);
            //Console.WriteLine(rect);
            //SimpleNumbers.PlayBingo();

            //Console.WriteLine(PI.AsArray(1000));
            Euromilhoes();
        }

        private static void MultAndDivTest(int maxCases = 5)
        { 
            Random rand = new Random();
            for (int i = 0; i < maxCases; i++)
            {
                double num1 = rand.Next(1, 11);
                double num2 = rand.Next(1, 11);
                Tuple<double, double> t = SimpleNumbers.MultAndDiv(num1, num2); 
                Console.WriteLine($"MultAndDiv {num1,3},{num2,3} -> { t.Item1,3 }, { t.Item2, 3}");
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

        static void Euromilhoes()
        {
            int[] chave = new int[5];
            int[] estrelas = new int[2];

            NotRepeatedArray(chave, 1, 50);
            NotRepeatedArray(estrelas, 1, 12);
            

        }

        private static void NotRepeatedArray(int[] arr, int min, int max)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                bool done = false;
                while (!done)
                {
                    int num = new Random().Next(min, max+1);
                    if (!Contains(arr, num)) //System.Linq
                    {
                        arr[i] = num;
                        done = true;
                    }
                }
            }
            Array.Sort(arr);         
        }

        private static bool Contains<T>(T[] arr, T trgt)
        {
            bool result = false;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Equals(trgt))
                    result = true;
            }
            return result;
        }

        private static void PrintArray<T>(T[] arr)
        {
            
        }
    }
}
