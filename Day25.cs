using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Advent
{
    class Point : IComparer<Point>, IComparable<Point>
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

        int IComparer<Point>.Compare(Point x, Point y)
        {
            Point origin = new Point(0, 0, 0, 0);
            return x.findManhattanDistance(origin) - y.findManhattanDistance(origin);
        }

        int IComparable<Point>.CompareTo(Point other)
        {
            Point origin = new Point(0, 0, 0, 0);
            return this.findManhattanDistance(origin) - other.findManhattanDistance(origin);
        }
    }

    class TreeNode
    {
        public Point self;
        public TreeNode leftChild;
        public TreeNode rightChild;

        public TreeNode(Point self, TreeNode leftChild, TreeNode rightChild)
        {
            this.self = self;
            this.leftChild = leftChild;
            this.rightChild = rightChild;
        }
    }

    class Day25
    {
        static List<int []> allPoints = new List<int[]>();
        static List<Point> pointList = new List<Point>();
        static Dictionary<Point, List<Point>> pointsInRange = new Dictionary<Point, List<Point>>();
        static Dictionary<Point, HashSet<Point>> nearbyPoints = new Dictionary<Point, HashSet<Point>>();
        static Dictionary<Point, HashSet<Point>> visitedPoints = new Dictionary<Point, HashSet<Point>>();

        public static void Run()
        {
            Console.WriteLine("\nDay 25");

            List<HashSet<Point>> constellations = new List<HashSet<Point>>();
            using (StreamReader reader = new StreamReader("input/input25.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    allPoints.Add(line.Split(',').Select(x => int.Parse(x)).ToArray());
            }
            //Point medianX = allPoints.OrderBy(x => x.x).Skip(allPoints.Count / 2).First();

            //allPoints.Sort();
            foreach(int[] point in allPoints)
            {
                pointList.Add(new Point(point[0], point[1], point[2], point[3]));
            }

            for (int i = 0; i < pointList.Count; i++)
            {
                pointsInRange.Add(pointList[i], new List<Point>());
                for (int j = 0; j < pointList.Count; j++)
                {
                    if (pointList[i].findManhattanDistance(pointList[j]) <= 3)
                        pointsInRange[pointList[i]].Add(pointList[j]);
                }
            }

            for (int i = 0; i < pointsInRange.Count; i++)
            {
                visitedPoints.Add(pointList[i], new HashSet<Point>());
                nearbyPoints.Add(pointList[i], new HashSet<Point>());
                foreach (Point p in pointsInRange[pointList[i]])
                {
                    addNeighborsOfNeighbor(i, p);
                }
            }

            //

            for(int index = 0; index < pointList.Count; index++)
            {
                if (constellations.Count == 0)
                    constellations.Add(new HashSet<Point>(nearbyPoints[pointList[index]]));
                else
                {
                    bool fitsExistingConstellation = false;
                    foreach (HashSet<Point> constellation in constellations)
                    {
                        foreach(Point point in nearbyPoints[pointList[index]])
                        {
                            if(constellation.Contains(point))
                            {
                                fitsExistingConstellation = true;
                                break;
                            }
                        }
                        if (fitsExistingConstellation)
                            break;
                    }
                    if (!fitsExistingConstellation)
                        constellations.Add(new HashSet<Point>(nearbyPoints[pointList[index]]));
                }
            }
            Console.WriteLine(constellations.Count);
        }

        static void addNeighborsOfNeighbor(int index, Point neighbor)
        {
            if (!visitedPoints[pointList[index]].Add(neighbor))
                return;
            nearbyPoints[pointList[index]].Add(neighbor);
            foreach(Point p in pointsInRange[neighbor])
            {
                nearbyPoints[pointList[index]].Add(p);
                addNeighborsOfNeighbor(index, p);
            }
        }
    }
}
