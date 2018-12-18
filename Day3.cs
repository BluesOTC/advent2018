using System;
using System.Collections.Generic;
using System.IO;

namespace Advent
{
    class Day3
    {
        public static void Run()
        {
            Console.WriteLine("\nDay 3");

            List<string> input = new List<string>();
            using (StreamReader reader = new StreamReader("input/input3.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    input.Add(line);
            }

            HashSet<Coordinate> claims = new HashSet<Coordinate>();
            HashSet<Coordinate> conflicts = new HashSet<Coordinate>();
            foreach (string line in input)
            {
                string[] splitLine = line.Split(new char[] { ' ', ',', '@', ':', 'x' });
                int x1 = Int32.Parse(splitLine[3]);
                int y1 = Int32.Parse(splitLine[4]);
                int x2 = x1 + Int32.Parse(splitLine[6]);
                int y2 = y1 + Int32.Parse(splitLine[7]);

                for (int x = x1; x < x2; x++)
                {
                    for (int y = y1; y < y2; y++)
                    {
                        Coordinate curr = new Coordinate(x, y);
                        if (!claims.Add(curr))
                            conflicts.Add(curr);
                    }
                }
            }
            Console.WriteLine("Conflicts: " + conflicts.Count);

            //Day 3-2
            foreach (string line in input)
            {
                string[] splitLine = line.Split(new char[] { ' ', ',', '@', ':', 'x' });
                if (!hasAnyConflicts(Int32.Parse(splitLine[3]), Int32.Parse(splitLine[4]), Int32.Parse(splitLine[6]), Int32.Parse(splitLine[7]), conflicts))
                {
                    Console.Write("Notable Claim: " + splitLine[0] + "\n");
                    break;
                }
            }
        }

        static bool hasAnyConflicts(int x, int y, int w, int h, HashSet<Coordinate> conflicts)
        {
            int x2 = x + w;
            int y2 = y + h;
            for (int x1 = x; x1 < x2; x1++)
                for (int y1 = y; y1 < y2; y1++)
                    if (conflicts.Contains(new Coordinate(x1, y1)))
                        return true;
            return false;
        }
    }
}
