using System;
using System.Linq;

namespace Exercicios
{
    class Program
    {
        static void Main(string[] args)
        {
            bool quit = false;
            while (!quit)
            {
                ApresentarMenu();
                quit = ReceberEscolha();
            }
        }
        private static void ApresentarMenu()
        {
            Console.Clear();
            Console.WriteLine("01) TripliSoma");
            Console.WriteLine("02) Mult e Div");
            Console.WriteLine("03) Advinha o número");
            Console.WriteLine(" ) 30's 40's");
            Console.WriteLine("\t04) while");
            Console.WriteLine("\t05) for");
            Console.WriteLine("\t06) foreach");
            Console.WriteLine("07) Bingo");
            Console.WriteLine("08) Euromilhões");
            Console.WriteLine(" ) PI");
            Console.WriteLine("\t09) Gregory-Leibniz Series");
            Console.WriteLine("\t10) Nilakantha Series");
            Console.WriteLine("\t11) As Array");
            Console.WriteLine("12) NumeroInteiro");
            Console.WriteLine("00) SAIR");
            Console.Write("Escolha a opção > ");
        }
        private static bool ReceberEscolha()
        {
            bool quitProgram = false;
            bool choice = false;
            while (!choice)
            {
                if(int.TryParse(Console.ReadLine(), out int input))
                {
                    switch (input)
                    {
                        case 1:
                            SumOrTripleTest();
                            break;
                        case 2:
                            MultAndDivTest();
                            break;
                        case 3:
                            SimpleNumbers.GuessingGame();
                            break;
                        case 4:
                            SimpleNumbers.FindTargetsWithWhile();
                            break;
                        case 5:
                            SimpleNumbers.FindTargetsWithFor();
                            break;
                        case 6:
                            SimpleNumbers.FindTargetsWithForeach();
                            break;
                        case 7:
                            SimpleNumbers.PlayBingo();
                            break;
                        case 8:
                            Euromilhoes();
                            break;
                        case 9:
                            TestPIGL();
                            break;
                        case 10:
                            TestPIN();
                            break;
                        case 11:
                            TestPIAsArray();
                            break;
                        case 12:
                            TestNumeroInteiro();
                            break;
                        case 0:
                            quitProgram = true;
                            break;
                        default:
                            Console.WriteLine("Essa opção não existe!");
                            break;
                    }
                    if (input != 0)
                    {
                        Console.Write("Prima ENTER para continuar...");
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("Input inválido. Prima qualquer tecla para continuar");
                    Console.ReadLine();
                }
                choice = true;
            }
            return quitProgram;
        }
        private static void MultAndDivTest(int maxCases = 5)
        { 
            Random rand = new Random();
            for (int i = 0; i < maxCases; i++)
            {
                double num1 = rand.Next(1, 11);
                double num2 = rand.Next(1, 11);
                Tuple<double, double> t = SimpleNumbers.MultAndDiv(num1, num2); 
                Console.WriteLine($"MultAndDiv {num1,3},{num2,3} -> Multiplication: { t.Item1,3 }, Division: { t.Item2, 3}");
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
        static void Euromilhoes()
        {
            int[] chave = new int[5];
            int[] estrelas = new int[2];

            NotRepeatedArray(chave, 1, 50);
            NotRepeatedArray(estrelas, 1, 12);
            Console.WriteLine($"Chave:    {ArrayAsString(chave)}");
            Console.WriteLine($"Estrelas: {ArrayAsString(estrelas)}");
        }
        static void TestPIGL()
        {
            Console.Write($"Qual a precisão que deseja? (0 - {int.MaxValue})> ");
            int precision = SimpleNumbers.GetIntFromUser(0, int.MaxValue);
            Console.WriteLine(PI.AsDoubleGL(precision));
        }
        static void TestPIN()
        {
            Console.Write($"Qual a precisão que deseja? (0 - {int.MaxValue})> ");
            int precision = SimpleNumbers.GetIntFromUser(0, int.MaxValue);
            Console.WriteLine(PI.AsDoubleN(precision));
        }
        static void TestPIAsArray()
        {
            Console.Write($"Qual a precisão que deseja? (0 - 1000)> ");
            int precision = SimpleNumbers.GetIntFromUser(0, 1000);
            var arr = PI.AsArray(precision);
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine($"{i}: {arr[i]}");
            }
        }
        static void TestNumeroInteiro()
        {
            Console.Write("Insira um numero inteiro > ");
            var num = new NumeroInteiro(SimpleNumbers.GetIntFromUser());
            string paridade = num.EPar() ? "par" : "impar";
            string sinal = num.EPositivo() ? "positivo" : "negativo";
            string primo = num.EPrimo() ? "é primo" : "não é primo";
            string ordem = num.Ordem();
            Console.WriteLine($"O número que introduziu foi {num.Numero}. Este número é {paridade}, {sinal}, {primo}, e a ordem é {ordem}.");
        }
        static void NotRepeatedArray(int[] arr, int min, int max)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                bool done = false;
                while (!done)
                {
                    int num = new Random().Next(min, max+1);
                    if (!arr.Contains(num)) //System.Linq
                    {
                        arr[i] = num;
                        done = true;
                    }
                }
            }
            Array.Sort(arr);         
        }
        static string ArrayAsString<T>(T[] arr)
        {
            string result = string.Empty;
            foreach (T val in arr)
                result += $"{val,2}, ";
            return result.Trim().TrimEnd(',');
        }
    }
}
