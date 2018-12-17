using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

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
            string[] splitLine = input.Split(' ');
            {
                List<char> polymer = input.ToCharArray().ToList();
                bool wasAnyRemoved = true;
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
                Console.WriteLine("Polymer Length: " + polymer.Count);
            }

            //Part 2
            char[] unitTypes = input.ToUpper().ToCharArray().Distinct().ToArray();
            int minLength = input.Length;
            char bestChar = '\0';
            foreach (char candidate in unitTypes)
            {
                bool wasAnyRemoved = true;
                List<char> polymer = input.Where(x => x != candidate && x != candidate + 32).ToList();
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
                if(polymer.Count < minLength)
                {
                    minLength = polymer.Count;
                    bestChar = candidate;
                }
            }
            Console.WriteLine("Unit removed: " + bestChar + ", Polymer Length: " + minLength);
        }
    }
}
