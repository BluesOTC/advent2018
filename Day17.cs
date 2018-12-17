using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent
{
    class Day17
    {
        public static void Run(List<string> input)
        {
            List<char[]> grid = new List<char[]>();
            for(int y = 0; y < 1896; y++)
            {
                grid.Add(new string('.', 200).ToCharArray());
            }
            grid[0][45] = '+';

            foreach(string line in input) //X range from 466-653, Y range from 6-1895, spring at 500,0
            {
                string[] coordinates = line.Split(new char[] { '=', '.', ',' });
                char coord1 = coordinates[0][0];
                char coord2 = coordinates[2][0];
                if(coord1 == 'x')
                {
                    int x = Int32.Parse(coordinates[1]);
                    int ystart = Int32.Parse(coordinates[3]);
                    int yend = Int32.Parse(coordinates[5]);
                    for(;ystart < yend; ystart++)
                    {
                        grid[ystart][x-455] = '#';
                    }
                }
                else
                {
                    int y = Int32.Parse(coordinates[1]);
                    int xstart = Int32.Parse(coordinates[3]);
                    int xend = Int32.Parse(coordinates[5]);
                    for (; xstart < xend; xstart++)
                    {
                        grid[y][xstart-455] = '#';
                    }
                }
            }

            foreach (char[] line in grid)
                Console.WriteLine(new string(line));

            //flow down until clay floor is reached
            //check for walls on both sides
            //if not enclosed, switch to |
            //can only layer on ~ or #
        }

        int floodFill(int x, int y, ref List<char[]> grid)
        {
            if (x < 0 || y < 0 || x >= grid[y].Length || y >= grid.Count)
                return 0;
            if (grid[y][x] == '#' || grid[y][x] == '~' || grid[y][x] == '|')
                return 0;
            return 1 + floodFill(x + 1, y, ref grid) + floodFill(x - 1, y, ref grid) + floodFill(x, y + 1, ref grid);
        }
    }
}
