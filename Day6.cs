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
            Console.WriteLine();
            Console.WriteLine("Day 6");

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
            int maxY = coordinates.Max(x => x[1]);
            int[][] grid = new int[coordinates.Max(x => x[0]) - minX + 2][]; //42, 347
            for (int i = 0; i < grid.Length; i++)
            {
                if (i == 0 || i == grid.Length)
                    grid[i] = Enumerable.Repeat(-2, maxY - minY + 2).ToArray();
                else
                {
                    grid[i] = Enumerable.Repeat(-1, maxY - minY + 2).ToArray();
                    grid[i][0] = -2;
                    grid[i][grid[i].Length - 1] = -2;
                }
            }

            //Day 6-1
            for (int row = 1; row < grid.Length; row++)
            {
                for (int col = 1; col < grid[row].Length; col++)
                {
                    int minDistance = Int32.MaxValue;
                    int closestIndex = -1;
                    for (int index = 0; index < coordinates.Count; index++)
                    {
                        int distance = findManhattanDistance(row + minX - 1, col + minY - 1, coordinates[index][0], coordinates[index][1]);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            closestIndex = index;
                        }
                        else if (distance == minDistance)
                            closestIndex = -1;
                    }
                    grid[row][col] = closestIndex;
                }
            }

            HashSet<int> infinites = new HashSet<int>();
            for (int col = 1; col < grid[0].Length; col++)
            {
                infinites.Add(grid[1][col]);
                infinites.Add(grid[grid.Length - 2][col]);
            }

            for (int row = 1; row < grid.Length; row++)
            {
                infinites.Add(grid[row][1]);
                infinites.Add(grid[row][grid[0].Length - 2]);
            }

            for (int r = 1; r < grid.Length; r++)
            {
                for (int c = 1; c < grid[r].Length; c++)
                {
                    if (infinites.Contains(grid[r][c]))
                        grid[r][c] = -2;
                }
            }

            List<int> areas = Enumerable.Repeat(0, coordinates.Count).ToList();
            foreach (int[] row in grid)
            {
                foreach (int point in row)
                {
                    if (point >= 0)
                        areas[point]++;
                    //Console.Write(point);
                    /*if (point >= 0 && point < 10)
                        Console.Write(" ");
                    Console.Write(",");*/
                }
                //Console.Write("\n");
            }

            Console.WriteLine("Max Area: " + areas.Max());

            //Day 6-2
            int area = 0;
            for (int r = 1; r < grid.Length - 1; r++)
            {
                for (int c = 1; c < grid[r].Length - 1; c++)
                {
                    int sum = 0;
                    for (int index = 0; index < coordinates.Count; index++)
                        sum += findManhattanDistance(r + minX - 1, c + minY - 1, coordinates[index][0], coordinates[index][1]);
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
