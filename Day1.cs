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

            List<string> input = new List<string>();
            using (StreamReader reader = new StreamReader("input1.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    input.Add(line);
            }

            int sum = 0;
            foreach (string line in input)
                sum += Int32.Parse(line.Substring(1, line.Length - 1)) * (line.IndexOf("+") == 0 ? 1 : -1);
            Console.WriteLine("Sum: " + sum);

            List<int> addends = new List<int>();
            foreach (string line in input)
            {
                addends.Add(Int32.Parse(line.Substring(1, line.Length - 1)) * (line.IndexOf("+") == 0 ? 1 : -1));
            }

            HashSet<int> values = new HashSet<int>();
            sum = 0;
            for (int index = 0; !values.Contains(sum); index++)
            {
                if (index > 0)
                    values.Add(sum);
                sum += addends[index % addends.Count];
            }
            Console.WriteLine("First duplicate:" + sum);
        }
    }
}
