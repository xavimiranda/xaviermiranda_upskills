using System;

namespace p2
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal num1 = GetDecimalFromUser();
            decimal num2 = GetDecimalFromUser();
            decimal? result = GetOpFromUser(num1, num2);
            if (result != null)
                Console.WriteLine($"O resultado é {result}");

        }
        private static decimal GetDecimalFromUser()
        {
            decimal num;
            Console.Write("Por favor introduza um número real: ");
            while (true)
            {
                if (decimal.TryParse(Console.ReadLine(), out num))
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
        
        private static decimal? GetOpFromUser(decimal num1, decimal num2)
        {
            Console.Write("Pretende multiplicar (*) ou dividir (/): ");
            decimal result;
            while (true) 
            {
                string op = Console.ReadLine();
                if (op != "/" && op != "*")
                    Console.WriteLine("Operação tem que ser multiplicação (*) ou divisão (/)");
                else if (op == "/" && num2 == 0)
                {
                    Console.WriteLine("Divisão por zero!");
                    return null;   
                }
                else
                {
                    result = op == "*" ? num1 * num2 : num1/num2;
                    return result;
                }
            }
        }
    }
}
