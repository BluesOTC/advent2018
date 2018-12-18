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
            int count2 = 0;
            int count3 = 0;

            List<string> input = new List<string>();
            using (StreamReader reader = new StreamReader("input/input2.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    input.Add(line);
                    IEnumerable<IGrouping<char, char>> groupedInput = line.GroupBy(x => x);
                    if (groupedInput.Any(x => x.Count() == 2))
                        count2++;
                    if (groupedInput.Any(x => x.Count() == 3))
                        count3++;
                }
            }
            Console.WriteLine("checksum: " + count2 * count3);

            //Day 2-2
            for (int index = 0; index < input[0].Length; index++)
            {
                List<string> candidates = new List<string>();
                foreach (string line in input)
                    candidates.Add(line.Remove(index, 1));

                IGrouping<string, string> prototypeBoxes;
                if ((prototypeBoxes = candidates.GroupBy(x => x).FirstOrDefault(x => x.Count() == 2)) != null)
                {
                    Console.WriteLine("Common letters: " + prototypeBoxes.Key);
                    break;
                }
            }
        }
    }
}
