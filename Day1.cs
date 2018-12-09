using System;
using System.Collections.Generic;

namespace Advent
{
    class Day1
    {
        public static void Run(List<string> input)
        {
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
