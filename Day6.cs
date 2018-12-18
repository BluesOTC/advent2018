using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Advent
{
    class Day6
    {
        public static void Run()
        {
            Console.WriteLine("\nDay 6");

            List<string> input = new List<string>();
            using (StreamReader reader = new StreamReader("input6.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    input.Add(line);
            }

            List<int[]> coordinates = new List<int[]>();
            foreach (string line in input)
            {
                string[] split = line.Split(',');
                coordinates.Add(new int[] { Int32.Parse(split[0]), Int32.Parse(split[1].TrimStart(' ')) });
            }

            int minX = coordinates.Min(x => x[0]);
            int minY = coordinates.Min(x => x[1]);

            foreach (int[] coordinate in coordinates)
            {
                coordinate[0] -= minX;
                coordinate[1] -= minY;
            }

            int maxY = coordinates.Max(x => x[1]);
            int[][] grid = new int[coordinates.Max(x => x[0])][]; //42, 347
            for (int i = 0; i < grid.Length; i++)
                grid[i] = new int[maxY];

            //Day 6-1
            for (int row = 0; row < grid.Length; row++)
            {
                for (int col = 0; col < grid[row].Length; col++)
                {
                    int minDistance = Int32.MaxValue;
                    int closestIndex = -1;
                    for (int index = 0; index < coordinates.Count; index++)
                    {
                        int distance = findManhattanDistance(row, col, coordinates[index][0], coordinates[index][1]);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            closestIndex = index;
                        }
                        else if (distance == minDistance)
                            closestIndex = -1;
                    }
                    grid[row][col] = closestIndex + 1;
                }
            }

            HashSet<int> infinites = new HashSet<int>();
            for (int col = 0; col < grid[0].Length; col++)
            {
                infinites.Add(grid[0][col]);
                infinites.Add(grid[grid.Length - 1][col]);
            }
            for (int row = 0; row < grid.Length; row++)
            {
                infinites.Add(grid[row][0]);
                infinites.Add(grid[row][grid[0].Length - 1]);
            }

            for (int r = 0; r < grid.Length; r++)
                grid[r] = grid[r].Select(x => infinites.Contains(x) ? 0 : x).ToArray();

            List<int> areas = Enumerable.Repeat(0, coordinates.Count + 1).ToList();
            foreach (int[] row in grid)
            {
                foreach (int point in row)
                {
                    if (point > 0)
                        areas[point]++;
                }
            }

            Console.WriteLine("Max Area: " + areas.Max());

            //Day 6-2
            int area = 0;
            for (int r = 0; r < grid.Length; r++)
            {
                for (int c = 0; c < grid[r].Length; c++)
                {
                    int sum = 0;
                    foreach(int[] coordinate in coordinates)
                    {
                        sum += findManhattanDistance(r, c, coordinate[0], coordinate[1]);
                        if (sum >= 10000)
                            break;
                    }
                    if (sum < 10000)
                        area++;
                }
            }

            Console.WriteLine("Safe Area Size: " + area);
        }

        static int findManhattanDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }
    }
}
