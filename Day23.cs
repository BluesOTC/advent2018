using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Advent
{
    class XYZCoordinate : Coordinate
    {
        public int z;

        public XYZCoordinate(int x, int y, int z) : base(x, y)
        {
            this.z = z;
        }

        public int findManhattanDistance(XYZCoordinate other)
        {
            return Math.Abs(this.x - other.x) + Math.Abs(this.y - other.y) + Math.Abs(this.z - other.z);
        }

        public int findManhattanDistance(int x, int y, int z)
        {
            return Math.Abs(this.x - x) + Math.Abs(this.y - y) + Math.Abs(this.z - z);
        }
    }

    class Nanobot : XYZCoordinate, IComparable<Nanobot>
    {
        public int signalRadius;

        public Nanobot(int x, int y, int z, int signalRadius) : base(x, y, z)
        {
            this.signalRadius = signalRadius;
        }

        int IComparable<Nanobot>.CompareTo(Nanobot other)
        {
            return this.signalRadius - other.signalRadius;
        }
    }

    class Octahedron : XYZCoordinate, IComparable<Octahedron>
    {
        public int signalRadius;
        public int botsInRange = 0;

        public Octahedron(int x, int y, int z, int signalRadius, List<Nanobot> botList) : base(x, y, z)
        {
            this.signalRadius = signalRadius;
            foreach (Nanobot bot in botList)
            {
                if (this.findManhattanDistance(bot) <= bot.signalRadius + this.signalRadius)
                    this.botsInRange++;
            }
            Console.WriteLine(String.Format("New Octahedron at {0},{1},{2} with a radius of {3} and {4} bots in range", x, y, z, signalRadius, botsInRange));
        }

        int IComparable<Octahedron>.CompareTo(Octahedron other)
        {
            int botDiff;
            if ((botDiff = other.botsInRange - this.botsInRange) == 0)
            {
                int distDiff;
                if((distDiff = this.findManhattanDistance(0, 0, 0) - other.findManhattanDistance(0, 0, 0)) == 0)
                {
                    if (this.x != other.x)
                        return this.x - other.x;
                    if (this.y != other.y)
                        return this.y - other.y;
                    if (this.z != other.z)
                        return this.z - other.z;
                }
            }
            return botDiff;
        }
    }

    class Day23
    {
        static List<Nanobot> botList = new List<Nanobot>();
        public static void Run()
        {
            Console.WriteLine("\nDay 23");

            int minX = 0, minY = 0, minZ = 0, maxX = 0, maxY = 0, maxZ = 0;
            using (StreamReader reader = new StreamReader("input/input23.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] nanobot = line.Split('=', '<', '>', ',');
                    Nanobot current;
                    botList.Add(current = new Nanobot(int.Parse(nanobot[2]), int.Parse(nanobot[3]), int.Parse(nanobot[4]), int.Parse(nanobot[7])));
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

            botList.Sort();
            Nanobot strongestNanobot = botList[botList.Count() - 1];
            Console.WriteLine(botList.Where(x => strongestNanobot.findManhattanDistance(x) <= strongestNanobot.signalRadius).Count());

            SortedSet<Octahedron> octahedra = new SortedSet<Octahedron>();
            octahedra.Add(new Octahedron((maxX + minX) / 2, (maxY + minY) / 2, (maxZ + minZ) / 2, Math.Max(Math.Max(maxX - minX, maxY - minY), maxZ - minZ) / 2, botList));
            while(octahedra.First().signalRadius > 1)
            {
                Octahedron curr = octahedra.First();
                Console.WriteLine("Current Octahedron radius: " + curr.signalRadius + ", Bots in Octahedron: " + curr.botsInRange);
                octahedra.Add(new Octahedron(curr.x - curr.signalRadius / 2, curr.y, curr.z, curr.signalRadius / 2, botList));
                octahedra.Add(new Octahedron(curr.x + curr.signalRadius / 2, curr.y, curr.z, curr.signalRadius / 2, botList));
                octahedra.Add(new Octahedron(curr.x, curr.y - curr.signalRadius / 2, curr.z, curr.signalRadius / 2, botList));
                octahedra.Add(new Octahedron(curr.x, curr.y + curr.signalRadius / 2, curr.z, curr.signalRadius / 2, botList));
                octahedra.Add(new Octahedron(curr.x, curr.y, curr.z - curr.signalRadius / 2, curr.signalRadius / 2, botList));
                octahedra.Add(new Octahedron(curr.x , curr.y, curr.z + curr.signalRadius / 2, curr.signalRadius / 2, botList));
                octahedra.Remove(curr);
            }

            Console.WriteLine(octahedra.First().findManhattanDistance(0, 0, 0));
        }

    }
}
