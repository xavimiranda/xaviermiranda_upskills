using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Exercicios
{
    class Dude
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public char CitizenLevel { get; set; }
        public override string ToString() => $"{Name, 10}, {Age, 3} [{CitizenLevel}]";
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Dude> denizens = new List<Dude>
            {
                new Dude {Name = "Xavier", Age = 32, CitizenLevel = 'B' },
                new Dude {Name = "Afonso", Age = 31, CitizenLevel = 'A'},
                new Dude {Name = "Tiago", Age = 34, CitizenLevel = 'C'}

            };

            foreach(Dude d in denizens.OrderBy(d => d.Name))
            {
                Console.WriteLine(d);
            }

            bool quit = false;
            while (!quit)
            {
                ApresentarMenu();
                quit = ReceberEscolha();
            }
        }
        static void arrayArePassedByRef(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = i * 3;
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
            Console.WriteLine("13) Ponto");
            Console.WriteLine("14) Calculadora");
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
                            TesteEuromilhoesLista();
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
                        case 13:
                            TestaPontos();
                            break;
                        case 14:
                            TestaCalculadora();
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

        private static void TestaCalculadora()
        {
            while(true)
            {
                Console.WriteLine("Introduza uma expressão aritmética válida. Introduza 'q' para sair");

                string input = Console.ReadLine();

                if (input == "q")
                    break;

                string[] parcelas = input.Split(' ');
                if (parcelas.Length != 3)
                    Console.WriteLine("Uma operação aritmética só aceita 3 elementos.");
                else
                {
                    double num1, num2;

                    if(!double.TryParse(parcelas[0], out num1))
                    {
                        Console.WriteLine("erro no num1");
                    }

                    if (!double.TryParse(parcelas[2], out num2))
                    {
                        Console.WriteLine("erro no num2");
                    }

                    var operacoes = new Regex("[-+*/]");
                    double result = 0d;
                    if (operacoes.IsMatch(parcelas[1]))
                    {
                        switch (parcelas[1])
                        {
                            case "+":
                                result = num1 + num2;
                                break;
                            case "-":
                                result = num1 - num2;
                                break;
                            case "/":
                                result = num1 / num2;
                                break;
                            case "*":
                                result = num1 * num2;
                                break;
                        }
                        Console.WriteLine(result);
                    }
                    else
                        Console.WriteLine("Essa operação não existe");
                }
            }
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
        static void TesteEuromilhoes()
        {
            var sorteio = Euromilhoes();
            Console.WriteLine($"Chave:    {ArrayAsString(sorteio[0])}");
            Console.WriteLine($"Estrelas: {ArrayAsString(sorteio[1])}");
        }
        static void TesteEuromilhoesLista()
        {
            var sorteio = EuromilhoesLista();
            string result = "Chave:    ";
                   
            foreach (int num in sorteio[0])
            {
                result += $"{num,2}, ";
            }
            Console.WriteLine(result.Trim());
            result = "Estrelas: ";
            foreach (int num in sorteio[1])
            {
                result += $"{num,2}, ";
            }
            Console.WriteLine(result.Trim());
        }
        static int[][] Euromilhoes()
        {
            int[][] sorteio = { new int[5], new int[2] };
        
            //int[] chave = new int[5];
            //int[] estrelas = new int[2];

            NotRepeatedArray(sorteio[0], 1, 50);
            NotRepeatedArray(sorteio[1], 1, 12);

            return sorteio;
        }
        static List<int>[] EuromilhoesLista()
        {
            List<int>[] sorteio = { new List<int>(), new List<int>() };
            var rnd = new Random();
            for (int i = 0; i < 5; i++)
            {
                while(true)
                {
                    int num = rnd.Next(1, 51);
                    if (!sorteio[0].Contains(num))
                    {
                        sorteio[0].Add(num);
                        break;
                    }
                    
                }
            }
            for (int i = 0; i < 2; i++)
            {
                while (true)
                {
                    int num = rnd.Next(1, 13);
                    if (!sorteio[1].Contains(num))
                    {
                        sorteio[1].Add(num);
                        break;
                    }
                }
            }

            return sorteio;
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
        static void TestaPontos()
        {
            Point p = new Point(1, 3);
            Console.WriteLine($"A abcissa do ponto p é {p.X}");
            Console.WriteLine($"A ordenada do ponto p é {p.Y}");
            Point p2 = new Point(2);
            if (p.Equals(p2))
                Console.WriteLine($"Pontos iguas, {p}");
            else
                Console.WriteLine($"Pontos diferentes. P1 => {p}, P2 => {p2}");
            
            char compSign = p.HasSmallerNorm(p2) ? '<' : '>';

            Console.WriteLine($"{p} tem norma {compSign} a {p2}");  
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
