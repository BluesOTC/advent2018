using System;
using System.Collections.Generic;

namespace Advent
{
    struct Coordinate : IEquatable<Coordinate>
    {
        public int x;
        public int y;

        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        bool IEquatable<Coordinate>.Equals(Coordinate other)
        {
            return this.x == other.x && this.y == other.y;
        }
    }

    class Day3
    {
        public static void Run(List<string> input)
        {
            HashSet<Coordinate> claims = new HashSet<Coordinate>();
            HashSet<Coordinate> conflicts = new HashSet<Coordinate>();
            foreach (string line in input)
            {
                string[] splitLine = line.Split(new char[] { ' ', ',', '@', ':', 'x' });
                int x = Int32.Parse(splitLine[3]);
                int y = Int32.Parse(splitLine[4]);

                for (int w = 0; w < Int32.Parse(splitLine[6]); w++)
                {
                    for (int h = 0; h < Int32.Parse(splitLine[7]); h++)
                    {
                        Coordinate curr = new Coordinate(x + w, y + h);
                        if (claims.Contains(curr))
                        {
                            if (!conflicts.Contains(curr))
                                conflicts.Add(curr);
                        }
                        else
                            claims.Add(curr);
                    }
                }
            }

            //Day 3-2
            foreach (string line in input)
            {
                string[] splitLine = line.Split(new char[] { ' ', ',', '@', ':', 'x' });
                if (!hasAnyConflicts(Int32.Parse(splitLine[3]), Int32.Parse(splitLine[4]), Int32.Parse(splitLine[6]), Int32.Parse(splitLine[7]), conflicts))
                    Console.Write("Notable Claim: " + splitLine[0] + "\n");
            }
            Console.WriteLine("Conflicts: " + conflicts.Count);
        }

        static bool hasAnyConflicts(int x, int y, int w, int h, HashSet<Coordinate> conflicts)
        {
            for (int i = x; i < x + w; i++)
                for (int j = y; j < y + h; j++)
                    if (conflicts.Contains(new Coordinate(i, j)))
                        return true;
            return false;
        }
    }
}
