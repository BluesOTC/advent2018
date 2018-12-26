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
            Console.WriteLine("\nDay 5");

            StringBuilder polymer;
            using (StreamReader reader = new StreamReader("input/input5.txt"))
                polymer = new StringBuilder(reader.ReadLine());

            //Part 1
            for (int index = 0; index < polymer.Length - 2; index++)
            {
                if (Math.Abs(polymer[index] - polymer[index + 1]) == 32)
                {
                    polymer.Remove(index, 2);
                    index -= index > 1 ? 2 : 1; //check if preceding base is now paired
                }
            }
            Console.WriteLine("Polymer Length: " + polymer.Length);

            //Part 2
            int minLength = polymer.Length;
            char[] copy = new char[polymer.Length];
            polymer.CopyTo(0, copy, 0, polymer.Length);
            for (char candidate = 'A'; candidate <= 'Z'; candidate++)
            {
                polymer = new StringBuilder(new string(copy)).Replace(candidate + "", null).Replace((candidate + "").ToLower(), null);
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
