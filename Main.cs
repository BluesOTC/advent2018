using System.IO;
using System.Collections.Generic;

namespace Advent
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> input = new List<string>();
            using (StreamReader reader = new StreamReader("input.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    input.Add(line);
            }

            Day14.Run(input);
        }
    }
}
