using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int start = (this.y * getRackID() + serial) * getRackID();
            start /= 100;
            int digit = start % 10;
            return digit - 5;
        }
    }

    class Day11
    {
        public static void Run(List<string> input)
        {
            int serial = 7689;
            int gridTotal = 0;
            int[][] grid = new int[300][];
            for (int i = 0; i < 300; i++)
                grid[i] = Enumerable.Repeat(0, 300).ToArray();
            for (int i = 1; i < 301; i++)
            {
                for (int j = 1; j < 301; j++)
                {
                    grid[i - 1][j - 1] = (new PowerCoordinate(i, j)).getPower(serial);
                    gridTotal += grid[i - 1][j - 1];
                }
            }

            int[][] candidates = new int[298][];
            for (int i = 0; i < 298; i++)
                candidates[i] = Enumerable.Repeat(0, 298).ToArray();
            int maxPower = Int32.MinValue;
            Coordinate maxCorner = new Coordinate(-1, -1);

            for (int i = 0; i < candidates.Length; i++)
            {
                for (int j = 1; j < candidates.Length; j++)
                {
                    candidates[i][j] = grid[i][j] + grid[i][j + 1] + grid[i][j + 2] + grid[i + 1][j] + grid[i + 1][j + 1] + grid[i + 1][j + 2] + grid[i + 2][j] + grid[i + 2][j + 1] + grid[i + 2][j + 2];
                    if (candidates[i][j] > maxPower)
                    {
                        maxPower = candidates[i][j];
                        maxCorner = new Coordinate(i + 1, j + 1);
                    }
                }
            }
            
            int maxLength = 3;
            for (int length = 4; length < 286; length++) //max power at 3x3 was 31, so 3x3 is the minimum possible optimal size. total of all grid power values was -40310, so the largest possible area is (90000 - 40341/5) square units, or 286x286 or smaller
            {
                int maxPowerLength = Int32.MinValue;
                Coordinate maxCornerLength = new Coordinate(-1, -1);
                for (int i = 0; i < candidates.Length - length; i++)
                {
                    for (int j = 0; j < candidates.Length - length; j++)
                    {
                        for (int iIndex = 0; iIndex < length; iIndex++)
                            candidates[i][j] += grid[i + iIndex][j + length - 1];
                        for (int jIndex = 0; jIndex < length; jIndex++)
                            candidates[i][j] += grid[i + length - 1][j + jIndex];

                        if (candidates[i][j] > maxPowerLength)
                        {
                            maxPowerLength = candidates[i][j];
                            maxCornerLength = new Coordinate(i + 1, j + 1);
                        }
                    }
                }

                if (maxPowerLength > maxPower)
                {
                    maxPower = maxPowerLength;
                    maxCorner = maxCornerLength;
                    maxLength = length;
                }
            }
            Console.WriteLine(String.Format("{0}, {1}", maxCorner.ToString(), maxLength));
        }
    }
}
