using System;

namespace p1
{
    class Program
    {
        static void Main(string[] args)
    {
            int num1 = 0;
            int num2 = 0;

            num1 = GetInputFromUser();
            num2 = GetInputFromUser();
            Console.WriteLine($"Os números introduzido foram {num1} e {num2}");
            if (num1 == num2)
            {
                Console.WriteLine($"Os números são iguais e o seu triplo é {num2*3}.");
            }
            else
            {
                Console.WriteLine($"A soma dos números é {num1+num2}");
            }
            Console.ReadLine();
        }

        private static int GetInputFromUser()
        {
            int num;
            Console.Write("Por favor introduza um número inteiro: ");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out num))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Por favor introduza um número válido");
                }

            }

            return num;
        }
    }
}
