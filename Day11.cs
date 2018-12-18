using System;
using System.IO;
using System.Linq;

namespace Advent
{
    class PowerCoordinate : Coordinate
    {
        public PowerCoordinate(int x, int y) : base(x, y) { }

        int getRackID()
        {
            return this.x + 10;
        }

        public int getPower(int serial)
        {
            int start = (this.y * getRackID() + serial) * getRackID() / 100;
            return start % 10 - 5;
        }
    }

    class Day11
    {
        public static void Run()
        {
            Console.WriteLine();
            Console.WriteLine("Day 11");

            int serial = 7689;
            int[][] grid = new int[300][];
            for (int i = 0; i < 300; i++)
                grid[i] = new int[300];
            for (int i = 1; i < 301; i++)
            {
                for (int j = 1; j < 301; j++)
                {
                    grid[i - 1][j - 1] = (new PowerCoordinate(i, j)).getPower(serial);
                    if (i > 1)
                    {
                        grid[i - 1][j - 1] += grid[i - 2][j - 1];
                        if (j > 1)
                        {
                            grid[i - 1][j - 1] += grid[i - 1][j - 2];
                            grid[i - 1][j - 1] -= grid[i - 2][j - 2];
                        }
                    }
                    else if (j > 1)
                        grid[i - 1][j - 1] += grid[i - 1][j - 2];
                }
            }

            int maxPower = Int32.MinValue;
            Coordinate maxCorner = null;

            int maxLength = 3;
            int length = 3;
            for (; length < grid.Length; length++)
            {
                int maxPowerLength = 0;
                Coordinate maxPowerCorner = null; 
                for (int i = length; i < grid.Length; i++)
                {
                    for (int j = length; j < grid[i].Length; j++)
                    {
                        int areaPower = grid[i][j];
                        if (i > length - 1)
                        {
                            areaPower -= grid[i - length][j];
                            if (j > length)
                            {
                                areaPower -= grid[i][j - length];
                                areaPower += grid[i - length][j - length];
                            }
                        }
                        else if (j > length - 1)
                            areaPower -= grid[i][j - length];
                        if (areaPower > maxPowerLength)
                        {
                            maxPowerLength = areaPower;
                            maxPowerCorner = new Coordinate(i + 2 - length, j + 2 - length);
                        }
                    }
                }
                if(length == 3)
                {
                    Console.WriteLine(String.Format("Part 1: {0}: Power = {1}", maxPowerCorner.ToString(), maxPowerLength));
                }

                if (maxPowerLength > maxPower)
                {
                    maxPower = maxPowerLength;
                    maxCorner = maxPowerCorner;
                    maxLength = length;
                }
            }
            Console.WriteLine(String.Format("Part 2: {0}, {1}: Power = {2}", maxCorner.ToString(), maxLength, maxPower));
        }
    }
}
