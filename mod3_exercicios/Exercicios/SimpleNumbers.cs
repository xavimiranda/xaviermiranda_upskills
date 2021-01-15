using System;
using System.Collections.Generic;
using System.Text;

namespace Exercicios
{
    public class SimpleNumbers
    {
        /// <summary>
        /// Returns the sum of the two given integers, if they are equal returns triple the sum
        /// </summary>
        public static int SumOrTriple(int num1, int num2) => num1 == num2 ? (num1 + num2) * 3 : (num1 + num2);
        ///<summary>Return multiplication and division of two given numbers</summary>
        public static Tuple<double, double> MultAndDiv(double num1, double num2) => Tuple.Create<double, double>(num1 * num2, num1 / num2);

        public static void GuessingGame()
        {
            Random rand = new Random();
            int target = rand.Next(1, 11);
            int numGuesses = 3;
            while (numGuesses != 0) 
            {
                Console.Write($"Tem {numGuesses} tentativas para adivinhar um número de 1 a 10 > ");
                int guess = GetIntFromUser(1, 10);
                if (guess == target)
                { 
                    Console.WriteLine("Acertou");
                    return;
                }
                else
                    numGuesses--;
            }
            Console.WriteLine($"Errou. O número era {target}");
        }

        public static void FindTargetsWithWhile(int[] numbers)
        {
            int index = 0;
            while (index < numbers.Length)
            {
                if (numbers[index] == 30 || numbers[index] == 40)
                {
                    Console.WriteLine($"Array contains one of the targets ({numbers[index]}).");
                    break;
                }
                else if (index == numbers.Length - 1)
                {
                    Console.WriteLine("Nothing Found!");
                }
                index++;
            }
        }
        public static void FindTargetsWithWhile()
        {
            FindTargetsWithWhile(GetArrayIntFromUser());
        }

        public static void FindTargetsWithFor(int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == 30 || numbers[i] == 40)
                {
                    Console.WriteLine($"One of the targets found ({numbers[i]})");
                    break;
                }
                else if (i == numbers.Length - 1)
                    Console.WriteLine("Nothing Found");
            }
        }
        public static void FindTargetsWithForeach(int[] numbers)
        {
            bool foundTarget = false;
            foreach (int num in numbers)
            {
                if (num == 30 || num == 40)
                {
                    Console.WriteLine($"Found a target ({num})");
                    foundTarget = true;
                    break;
                }
            }
            if (!foundTarget)
                Console.WriteLine("Didn't find any targets");
        }

        /// <summary>
        /// Tries to read an integer from the user and checks it is within the boudaries
        /// </summary>
        public static void PlayBingo(int numGuesses = 3)
        {
            Console.WriteLine( "================================BINGO======================================");
            Console.WriteLine($"Tem {numGuesses} tentativas para advinhar um número sorteado de -100 a 100.");
            int[] guesses = new int[numGuesses];
            //get 3 inputs from user
            for (int i = 0; i < numGuesses; i++)
            {
                Console.Write($"{i+1}ª > ");
                guesses[i] = GetIntFromUser(-100, 100);
            }
            //generate random number between -100 and 100
            var rand = new Random();
            int target = rand.Next(-100, 101);

            int closestGuess = int.MaxValue;
            int closesDist = int.MaxValue;

            foreach(int guess in guesses)
            {
                if(guess == target)
                {
                    Console.WriteLine($"BINGO! {target}");
                    return;
                }
                else
                {
                    if (Math.Abs(target - guess) < closesDist)
                    {
                        closesDist = Math.Abs(target - guess);
                        closestGuess = guess;
                    }
                }
            }
            Console.WriteLine($"A tentativa mais próxima de {target} foi {closestGuess}");
            Console.WriteLine("===========================================================================");
        }
        private static int GetIntFromUser(int min, int max)
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int guess) && (guess >= min && guess <= max))
                {
                    return guess;
                }
                else
                    Console.WriteLine("Invalid input. Please try again...");
            }
        }
        private static int[] GetArrayIntFromUser()
        {
            List<int> result = new List<int>();
            while (true)
            {
                Console.Write("Enter a number to insert into array or empty to finish > ");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                    break;
                else
                {
                    if (int.TryParse(input, out int num))
                        result.Add(num);
                    else
                        Console.WriteLine("Try Entering an integer...");
                }
            }
            return result.ToArray();
        }
    }
}
