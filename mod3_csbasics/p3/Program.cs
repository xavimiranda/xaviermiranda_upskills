using System;

namespace p3
{
    class Program
    {
        static void Main()
        {
            Random randGen = new Random();
            while (true)
            {
                int target = randGen.Next(1, 11);
                int guess = GetIntFromUser();

                Console.WriteLine(target == guess ? "Matched" : "Not Matched");
                Console.WriteLine("Again? Press 'q' to exit.");
                if (Console.ReadLine() == "q")
                    break;
            }
        }

        static int GetIntFromUser()
        {
            Console.Write("Please insert an integer from 1 to 10: ");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    if (input >= 1 && input <= 10)
                        return input;
                    else 
                        Console.WriteLine("The input must be between 1 and 10");
                }
                else
                    Console.WriteLine("Try inserting an integer.");
            }
        }
    }
}