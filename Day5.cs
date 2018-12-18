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
                input = reader.ReadLine();

            //Part 1
            StringBuilder polymer = new StringBuilder(input);
            for (int index = 0; index < polymer.Length - 2; index++)
            {
                if (Math.Abs(polymer[index] - polymer[index + 1]) == 32)
                {
                    polymer.Remove(index, 2);
                    index -= index > 1 ? 2 : 1;
                }
            }
            Console.WriteLine("Polymer Length: " + polymer.Length);

            //Part 2
            IEnumerable<char> unitTypes = input.ToUpper().ToCharArray().Distinct();
            int minLength = input.Length;
            foreach (char candidate in unitTypes)
            {
                polymer = new StringBuilder(input).Replace(candidate.ToString(), "").Replace((candidate + 32).ToString(), "");
                for (int index = 0; index < polymer.Length - 2; index++)
                {
                    if (Math.Abs(polymer[index] - polymer[index + 1]) == 32)
                    {
                        polymer.Remove(index, 2);
                        index -= index > 1 ? 2 : 1;
                    }
                }
                minLength = Math.Min(minLength, polymer.Length);
            }
            Console.WriteLine("Min Polymer Length: " + minLength);
        }
    }
}
