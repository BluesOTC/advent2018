using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Advent
{
    class Nanobot : Coordinate, IComparable<Nanobot>
    {
        public int signalRadius;
        public int z;
        public List<Nanobot> vertices = new List<Nanobot>();

        public Nanobot(int x, int y, int z, int signalRadius) : base(x, y)
        {
            this.z = z;
            this.signalRadius = signalRadius;
            vertices.Add(new Nanobot(x + signalRadius, y, z, 0));
            //Test all vertices for bots in range, then binary search the volume, pruning anything that's less than the max found
        }

        int IComparable<Nanobot>.CompareTo(Nanobot other)
        {
            return this.signalRadius - other.signalRadius;
        }

        public int findManhattanDistance(Nanobot other)
        {
            return Math.Abs(this.x - other.x) + Math.Abs(this.y - other.y) + Math.Abs(this.z - other.z);
        }

        public int findManhattanDistance(int x, int y, int z)
        {
            return Math.Abs(this.x - x) + Math.Abs(this.y - y) + Math.Abs(this.z - z);
        }
    }

    class Day23
    {
        public static void Run()
        {
            Console.WriteLine("\nDay 23");

            int minX = 0, minY = 0, minZ = 0, maxX = 0, maxY = 0, maxZ = 0;
            List<Nanobot> nanobots = new List<Nanobot>();
            using (StreamReader reader = new StreamReader("input/input23.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] nanobot = line.Split('=', '<', '>', ',');
                    Nanobot current;
                    nanobots.Add(current = new Nanobot(int.Parse(nanobot[2]), int.Parse(nanobot[3]), int.Parse(nanobot[4]), int.Parse(nanobot[7])));
                    if (current.x < minX)
                        minX = current.x;
                    if (current.y < minY)
                        minY = current.y;
                    if (current.z < minZ)
                        minZ = current.z;
                    if (current.x > maxX)
                        maxX = current.x;
                    if (current.y > maxY)
                        maxY = current.y;
                    if (current.z > maxZ)
                        maxZ = current.z;
                }
            }
            int maxBots = 0;
            int minDistance = int.MaxValue;

            nanobots.Sort();
            Nanobot strongestNanobot = nanobots[nanobots.Count() - 1];
            Console.WriteLine(nanobots.Where(x => strongestNanobot.findManhattanDistance(x) <= strongestNanobot.signalRadius).Count());
            int minRadius = nanobots[0].signalRadius;
            Console.WriteLine(nanobots[0].signalRadius);
            Console.WriteLine(minX);
            Console.WriteLine(minY);
            Console.WriteLine(minZ);
            Console.WriteLine(maxX);
            Console.WriteLine(maxY);
            Console.WriteLine(maxZ);

            for (int x = minX; x <= maxX; x++)
            {
                for(int y= minY; y <= maxY; y++)
                {
                    for(int z = minZ; z <= maxZ; z++)
                    {
                        int botsInRange = nanobots.Where(b => b.findManhattanDistance(x, y, z) < b.signalRadius).Count();
                        if (botsInRange > maxBots)
                        {
                            maxBots = botsInRange;
                            int distance = Math.Abs(x) + Math.Abs(y) + Math.Abs(z);
                            if (distance < minDistance)
                                minDistance = distance;
                        }
                    }
                }
            }

            Console.WriteLine(minDistance);
        }
    }
}
