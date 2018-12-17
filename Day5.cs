using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Advent
{
    class Day5
    {
        public static void Run(List<string> input)
        {
            Console.WriteLine();
            Console.WriteLine("Day 5");

            string[] splitLine;
            using (StreamReader reader = new StreamReader("input5.txt"))
            {
                splitLine = reader.ReadLine().Split(' ');
            }
            char[] unitTypes = input[0].ToUpper().ToCharArray().Distinct().ToArray();
            foreach (char candidate in unitTypes)
            {
                bool wasAnyRemoved = true;
                List<char> polymer = input[0].Where(x => x != candidate && x != candidate + 32).ToList(); //skip the removing in this line for 5-1
                while (wasAnyRemoved)
                {
                    wasAnyRemoved = false;
                    for (int index = 0; index < polymer.Count - 1; index++)
                    {
                        if (Math.Abs(polymer[index] - polymer[index + 1]) == 32)
                        {
                            polymer[index] = polymer[index + 1] = (char)1;
                            wasAnyRemoved = true;
                            index++;
                        }
                    }
                    polymer.RemoveAll(x => x == (char)1);
                }
                Console.WriteLine("Unit removed: " + candidate + ", Polymer Length: " + polymer.Count);
            }
        }
    }
}
