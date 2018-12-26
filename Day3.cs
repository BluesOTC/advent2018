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

            List<string[]> input = new List<string[]>();
            HashSet<Coordinate> claims = new HashSet<Coordinate>();
            HashSet<Coordinate> conflicts = new HashSet<Coordinate>();
            //HashSet<(int, bool)>
            using (StreamReader reader = new StreamReader("input/input3.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] splitLine = line.Split(new char[] { '#', ' ', ',', '@', ':', 'x' });
                    input.Add(splitLine);
                    int x1 = int.Parse(splitLine[4]);
                    int y1 = int.Parse(splitLine[5]);
                    int x2 = x1 + int.Parse(splitLine[7]);
                    int y2 = y1 + int.Parse(splitLine[8]);

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
            }
            Console.WriteLine("Conflicts: " + conflicts.Count);

            //Day 3-2
            foreach (string[] splitLine in input)
            {
                if (!hasAnyConflicts(int.Parse(splitLine[4]), int.Parse(splitLine[5]), int.Parse(splitLine[7]), int.Parse(splitLine[8]), conflicts))
                {
                    Console.Write("Notable Claim: " + splitLine[1] + "\n");
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

/*using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Advent
{
    class Day3
    {
        public static void Run()
        {
            Console.WriteLine("\nDay 3");


            Dictionary<Coordinate, int> claims = new Dictionary<Coordinate, int>();
            HashSet<Coordinate> conflicts = new HashSet<Coordinate>();
            HashSet<int> conflictedClaims = new HashSet<int>();

            int index = 0;
            using (StreamReader reader = new StreamReader("input/input3.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    index++;
                    string[] splitLine = line.Split(new char[] { '#', ' ', ',', '@', ':', 'x' });
                    int x1 = int.Parse(splitLine[4]);
                    int y1 = int.Parse(splitLine[5]);
                    int x2 = x1 + int.Parse(splitLine[7]);
                    int y2 = y1 + int.Parse(splitLine[8]);

                    for (int x = x1; x < x2; x++)
                    {
                        for (int y = y1; y < y2; y++)
                        {
                            Coordinate curr = new Coordinate(x, y);
                            if (claims.ContainsKey(curr))
                            {
                                if (conflicts.Add(curr))
                                    conflictedClaims.Add(claims[curr]);
                                conflictedClaims.Add(index);
                            }
                            else
                                claims.Add(curr, index);
                        }
                    }
                }
            }
            Console.WriteLine("Conflicts: " + conflicts.Count);
            Console.WriteLine("Notable Claim: " + claims.Values.Except(conflictedClaims).First());
        }
    }
}*/
