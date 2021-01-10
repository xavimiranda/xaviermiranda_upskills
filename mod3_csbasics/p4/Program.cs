using System;

namespace p4
{
    class Program
    {
        static void Main(string[] args)
        {
            // int[] numbers = { 1, 2, 3, 2, 30, 4, 5, 6, 40 };
            // int[] numbers = { 1, 2, 3, 2, 4, 5, 6, 40 };
            int[] numbers = { 1, 2, 3, 2, 4, 5, 6 };
            FindTargetsWithForeach(numbers);
            
        }

        private static void FindTargetsWithWhile(int[] numbers)
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
 
        private static void FindTargetsWithFor(int[] numbers)
        {
            for(int i = 0; i < numbers.Length; i++)
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
  
        private static void FindTargetsWithForeach(int[] numbers)
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
    }
}
