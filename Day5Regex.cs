using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent
{
    class Day5Regex
    {
        public static void Run(List<string> input)
        {
            //Regex nonsense. Slow!
            string pattern = @"(.)(?!\1)(?i:\1)";
            Regex comp10 = new Regex(pattern, RegexOptions.Compiled);

            string fullpolymer = input[0];
            {
                int startLength;
                do
                {
                    startLength = fullpolymer.Length;
                    fullpolymer = comp10.Replace(fullpolymer, "");
                } while (startLength > fullpolymer.Length);
            }
            Console.WriteLine("Polymer Length: " + fullpolymer.Length);

            for (char candidate = 'A'; candidate < 'Z' + 1; candidate++)
            {
                string polymer = string.Concat(fullpolymer.Where(x => x != candidate && x != candidate + 32));
                int startLength;
                do
                {
                    startLength = polymer.Length;
                    polymer = comp10.Replace(polymer, "");
                } while (startLength > polymer.Length);
                Console.WriteLine("Unit removed: " + candidate + ", Polymer Length: " + polymer.Length);
            }
        }
    }
}
