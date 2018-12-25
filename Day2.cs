using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace Advent
{
    class Day2
    {
        public static void Run()
        {
            Console.WriteLine("\nDay 2");
            long count2 = 0;
            long count3 = 0;

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
                HashSet<string> candidates = new HashSet<string>();
                foreach (string line in input)
                {
                    StringBuilder candidate = new StringBuilder(line.Substring(0, index));
                    candidate.Append(line.Substring(index + 1));
                    if (!candidates.Add(candidate.ToString()))
                    {
                        Console.WriteLine("Common letters: " + candidate.ToString());
                        return;
                    }
                }
            }
        }
    }
}
