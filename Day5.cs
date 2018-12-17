using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace Advent
{
    class Day5
    {
        public static void Run()
        {
            Console.WriteLine();
            Console.WriteLine("Day 5");

            string input;
            using (StreamReader reader = new StreamReader("input5.txt"))
            {
                input = reader.ReadLine();
            }

            //Part 1
            StringBuilder polymer = new StringBuilder(input);
            for (int index = 0; index < polymer.Length - 2; index++)
            {
                if (Math.Abs(polymer[index] - polymer[index + 1]) == 32)
                {
                    polymer.Remove(index, 2);
                    index--;
                    if (index > 0)
                        index--;
                }
            }
            Console.WriteLine("Polymer Length: " + polymer.Length);

            //Part 2
            char[] unitTypes = input.ToUpper().ToCharArray().Distinct().ToArray();
            int minLength = input.Length;
            char bestChar = '\0';
            foreach (char candidate in unitTypes)
            {
                polymer = new StringBuilder(input);
                for (int index = 0; index < polymer.Length - 2; index++)
                {
                    if (polymer[index] == candidate || polymer[index] == candidate + 32)
                    {
                        polymer.Remove(index, 1);
                        if (index > 0)
                            index--;
                        if (index == polymer.Length)
                            break;
                    }
                    if (Math.Abs(polymer[index] - polymer[index + 1]) == 32)
                    {
                        polymer.Remove(index, 2);
                        index--;
                        if (index > 0)
                            index--;
                    }
                }
                if (polymer.Length < minLength)
                {
                    minLength = polymer.Length;
                    bestChar = candidate;
                }
            }
            Console.WriteLine("Unit removed: " + bestChar + ", Polymer Length: " + minLength);
        }
    }
}
