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
            //Console.WriteLine(String.Format("New Octahedron at {0},{1},{2} with a radius of {3} and {4} bots in range", x, y, z, signalRadius, botsInRange));
        }

        int IComparable<Octahedron>.CompareTo(Octahedron other)
        {
            int botDiff;
            if ((botDiff = other.botsInRange - this.botsInRange) == 0)
            {
                int distDiff;
                if ((distDiff = this.findManhattanDistance(0, 0, 0) - other.findManhattanDistance(0, 0, 0)) == 0)
                {
                    if (this.signalRadius == other.signalRadius)
                        return this.GetHashCode() - other.GetHashCode();
                    return other.signalRadius - this.signalRadius; //search larger octahedra first if distance is tied since they'll contain closer points
                }
                else
                    return distDiff;
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

            int minX, minY, minZ, maxX, maxY, maxZ, radius;
            using (StreamReader reader = new StreamReader("input/input23.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] nanobot = line.Split('=', '<', '>', ',');
                    Nanobot current;
                    botList.Add(current = new Nanobot(int.Parse(nanobot[2]), int.Parse(nanobot[3]), int.Parse(nanobot[4]), int.Parse(nanobot[7])));
                }
            }

            minX = botList.Min(x => x.x);
            minY = botList.Min(x => x.y);
            minZ = botList.Min(x => x.z);
            maxX = botList.Max(x => x.x);
            maxY = botList.Max(x => x.y);
            maxZ = botList.Max(x => x.z);
            radius = (new List<int> { Math.Abs(minX), Math.Abs(minY), Math.Abs(minZ), maxX, maxY, maxZ }).Max();

            botList.Sort();
            Nanobot strongestNanobot = botList[botList.Count() - 1];
            Console.WriteLine(botList.Where(x => strongestNanobot.findManhattanDistance(x) <= strongestNanobot.signalRadius).Count());

            new Octahedron(21855350, 29211599, 27620767, 0, botList);

            SortedSet<Octahedron> octahedra = new SortedSet<Octahedron>();
            octahedra.Add(new Octahedron((maxX + minX) / 2, (maxY + minY) / 2, (maxZ + minZ) / 2, radius, botList));
            while (octahedra.First().signalRadius > 0)
            {
                Octahedron curr = octahedra.First();
                //Console.WriteLine("Current Octahedron radius: " + curr.signalRadius + ", Bots in Octahedron: " + curr.botsInRange);
                int nextRadius = curr.signalRadius * 2 / 3 + (curr.signalRadius * 2) % 3; //add back remainder to ensure greater coverage since their centers are misplaced
                if (nextRadius == curr.signalRadius) //integer division issues at low values
                    nextRadius--;
                if (curr.signalRadius > 1)
                {
                    octahedra.Add(new Octahedron(curr.x - curr.signalRadius / 2, curr.y, curr.z, nextRadius, botList));
                    octahedra.Add(new Octahedron(curr.x + curr.signalRadius / 2, curr.y, curr.z, nextRadius, botList));
                    octahedra.Add(new Octahedron(curr.x, curr.y - curr.signalRadius / 2, curr.z, nextRadius, botList));
                    octahedra.Add(new Octahedron(curr.x, curr.y + curr.signalRadius / 2, curr.z, nextRadius, botList));
                    octahedra.Add(new Octahedron(curr.x, curr.y, curr.z - curr.signalRadius / 2, nextRadius, botList));
                    octahedra.Add(new Octahedron(curr.x, curr.y, curr.z + curr.signalRadius / 2, nextRadius, botList));
                }
                else
                {
                    octahedra.Add(new Octahedron(curr.x + 1, curr.y, curr.z, 0, botList));
                    octahedra.Add(new Octahedron(curr.x - 1, curr.y, curr.z, 0, botList));
                    octahedra.Add(new Octahedron(curr.x, curr.y + 1, curr.z, 0, botList));
                    octahedra.Add(new Octahedron(curr.x, curr.y - 1, curr.z, 0, botList));
                    octahedra.Add(new Octahedron(curr.x, curr.y, curr.z + 1, 0, botList));
                    octahedra.Add(new Octahedron(curr.x, curr.y, curr.z - 1, 0, botList));
                }
                octahedra.Remove(curr);
            }

            Console.WriteLine(octahedra.First().findManhattanDistance(0, 0, 0));
        }

    }
}
