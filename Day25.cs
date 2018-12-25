using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Advent
{
    class Point
    {
        public int x, y, z, t;

        public Point(int x, int y, int z, int t)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.t = t;
        }

        public int findManhattanDistance(Point other)
        {
            return Math.Abs(this.x - other.x) + Math.Abs(this.y - other.y) + Math.Abs(this.z - other.z) + Math.Abs(this.t - other.t);
        }
    }

    class Day25
    {
        static List<Point> pointList = new List<Point>();
        static int[] parentID;
        static int[] rank;

        public static void Run()
        {
            Console.WriteLine("\nDay 25");
            
            using (StreamReader reader = new StreamReader("input/input25.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    int[] point = line.Split(',').Select(x => int.Parse(x)).ToArray();
                    pointList.Add(new Point(point[0], point[1], point[2], point[3]));
                }
            }
            parentID = Enumerable.Range(0, pointList.Count).ToArray();
            rank = Enumerable.Repeat(0, pointList.Count).ToArray();

            for (int i = 0; i < pointList.Count; i++)
            {
                for (int j = 0; j < pointList.Count; j++)
                {
                    if (pointList[i].findManhattanDistance(pointList[j]) <= 3)
                    {
                        Union(i, j);
                    }
                }
            }

            int constellations = 0;
            for (int index = 0; index < parentID.Length; index++)
            {
                if (parentID[index] == index)
                    constellations++;
            }

            Console.WriteLine(constellations);
        }

        static int Find(int i)
        {
            return parentID[i] == i ? i : Find(parentID[i]);
            //return parentID[i] == i ? i : parentID[i] = Find(parentID[i]);
        }

        static void Union(int i, int j)
        {
            int rootI = Find(i);
            int rootJ = Find(j);
            if (rootI == rootJ)
                return;
            if (rank[rootI] < rank[rootJ])
                parentID[rootI] = rootJ;
            else if (rank[rootI] > rank[rootJ])
                parentID[rootJ] = rootI;
            else
            {
                parentID[rootI] = rootJ;
                rank[rootJ]++;
            }
        }
    }
}
