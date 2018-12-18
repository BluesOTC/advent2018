using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Advent
{
    class Day2
    {
        public static void Run()
        {
            Console.WriteLine("\nDay 2");

            List<string> input = new List<string>();
            using (StreamReader reader = new StreamReader("input2.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    input.Add(line);
            }

            int count2 = 0;
            int count3 = 0;
            bool flag2 = false;
            bool flag3 = false;

            foreach(string line in input)
            {
                char[] inputLine = line.ToCharArray();
                List<char> candidates = inputLine.Distinct().ToList();
                foreach(char c in candidates)
                {
                    if(!flag2 && inputLine.Where(x => x == c).Count() == 2)
                    {
                        flag2 = true;
                        count2++;
                    }
                    else if (!flag3 && inputLine.Where(x => x == c).Count() == 3)
                    {
                        flag3 = true;
                        count3++;
                    }
                }
                flag2 = false;
                flag3 = false;
            }

            Console.WriteLine("checksum: " + count2 * count3);

            //Day 2-2
            for (int index = 0; index < input[0].Length; index++)
            {
                List<string> candidates = new List<string>();
                foreach (string line in input)
                    candidates.Add(line.Remove(index, 1));

                List<string> elements = candidates.Distinct().ToList();                
                if (candidates.Count() != elements.Count())
                {
                    foreach(string duplicate in elements)
                        candidates.Remove(duplicate);
                    Console.WriteLine("Common letters: " + candidates[0].ToString());
                    break;
                }
            }
        }
    }
}
