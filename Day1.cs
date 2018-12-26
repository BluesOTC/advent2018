using System;
using System.Collections.Generic;
using System.IO;

namespace Advent
{
    class Day1
    {
        public static void Run()
        {
            Console.WriteLine("Day 1");

            int sum = 0;
            List<int> addends = new List<int>();
            HashSet<int> values = new HashSet<int>();

            using (StreamReader reader = new StreamReader("input/input1.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    int addend = int.Parse(line.Substring(1, line.Length - 1)) * (line[0] == '+' ? 1 : -1);
                    sum += addend;
                    addends.Add(addend);
                    if (!values.Add(sum))
                        Console.WriteLine("First duplicate:" + sum);
                }
                Console.WriteLine("Initial sum:" + sum);
            }

            sum += addends[0];
            for (int index = 1; values.Add(sum); index = (index + 1) % addends.Count)
                sum += addends[index];
            Console.WriteLine("First duplicate:" + sum);
        }
    }
}
