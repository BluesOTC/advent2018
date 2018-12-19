using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Advent
{
    class Day17
    {
        public static void Run()
        {
            Console.WriteLine("\nDay 17");

            List<string> input = new List<string>();
            using (StreamReader reader = new StreamReader("input/input17.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    input.Add(line);
            }

            List<char[]> grid = new List<char[]>();

            int minY = Int32.MaxValue;
            foreach (string line in input) //X range from 466-653, Y range from 6-1895, spring at 500,0
            {
                string[] coordinates = line.Split(new char[] { '=', '.', ',' });
                char coord1 = coordinates[0][0];
                char coord2 = coordinates[2][0];
                if (coord1 == 'x')
                {
                    int x = Int32.Parse(coordinates[1]);
                    int ystart = Int32.Parse(coordinates[3]);
                    if (minY > ystart)
                        minY = ystart;
                    int yend = Int32.Parse(coordinates[5]);
                    while (grid.Count <= yend)
                        grid.Add(Enumerable.Repeat('.', 1000).ToArray());
                    for (; ystart <= yend; ystart++)
                    {
                        grid[ystart][x] = '#';
                    }
                }
                else
                {
                    int y = Int32.Parse(coordinates[1]);
                    while (grid.Count <= y)
                        grid.Add(Enumerable.Repeat('.', 1000).ToArray());
                    if (minY > y)
                        minY = y;
                    int xstart = Int32.Parse(coordinates[3]);
                    int xend = Int32.Parse(coordinates[5]);
                    for (; xstart <= xend; xstart++)
                    {
                        grid[y][xstart] = '#';
                    }
                }
            }
            grid[0][500] = '+';
            fillDown(500, 1, ref grid);

            int tiles = 0;
            int lakeTiles = 0;
            foreach (char[] line in grid)
            {
                //Console.WriteLine(new string(line).Substring(465, 198));
                lakeTiles += line.Where(x => x == '~').Count();
                tiles += line.Where(x => x == '|').Count();
            }
            Console.WriteLine("Tiles touched by water: " + (tiles - minY + 1 + lakeTiles));
            Console.WriteLine("Lake tiles: " + lakeTiles);
        }

        static void fillDown(int x, int y, ref List<char[]> grid)
        {
            int distance = 0;
            while (true)
            {
                grid[y + distance][x] = '|';
                if (y + distance + 1 == grid.Count || grid[y + distance + 1][x] == '|')
                    return;
                if (grid[y + distance + 1][x] == '#' || grid[y + distance + 1][x] == '~')
                {
                    fillHorizontally(x, y + distance, ref grid);
                    return;
                }
                distance++;
            }
        }

        static void fillHorizontally(int x, int y, ref List<char[]> grid)
        {
            int leftDistance = 0;
            bool leftWall = false;
            while (true)
            {
                if (grid[y][x - leftDistance] != '~')
                    grid[y][x - leftDistance] = '|';
                else
                {
                    leftWall = true;
                    break;
                }
                if (grid[y + 1][x - leftDistance] == '.' || grid[y + 1][x - leftDistance] == '|')
                {
                    fillDown(x - leftDistance, y, ref grid);
                    break;
                }
                if (grid[y][x - leftDistance - 1] == '#')
                {
                    leftWall = true;
                    break;
                }
                leftDistance++;
            }

            int rightDistance = 0;
            bool rightWall = false;
            while (true)
            {
                if (grid[y][x + rightDistance] != '~')
                    grid[y][x + rightDistance] = '|';
                else
                {
                    rightWall = true;
                    break;
                }
                if (grid[y + 1][x + rightDistance] == '.' || grid[y + 1][x + rightDistance] == '|')
                {
                    fillDown(x + rightDistance, y, ref grid);
                    break;
                }
                if (grid[y][x + rightDistance + 1] == '#')
                {
                    rightWall = true;
                    break;
                }
                rightDistance++;
            }

            if (rightWall && leftWall)
            {
                for (int index = x - leftDistance; index <= x + rightDistance; index++)
                    grid[y][index] = '~';
                fillHorizontally(x, y - 1, ref grid);
            }
        }
    }
}
