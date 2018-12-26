using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Advent
{
    class Day18
    {
        public static void Run()
        {
            Console.WriteLine("\nDay 18");

            char[] grid = new char[2600]; //1D array makes comparisons and linq operations easier but unintuitive, add padding of 50 on both sides to avoid bound checking
            using (StreamReader reader = new StreamReader("input/input18.txt"))
            {
                int row = 1;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    line.CopyTo(0, grid, 50 * row , 50);
                    row++;
                }
            }

            bool duplicateFound = false;
            int minutes = 1000000000;
            Dictionary<string, int> statesSeen = new Dictionary<string, int>();
            for (int minute = 0; minute < minutes; minute++)
            {
                char[] currState = new char[grid.Length];
                grid.CopyTo(currState, 0);
                string state;
                if (!duplicateFound) //if pattern has repeated, find last instance and extrapolate to t = 10^9
                {
                    if (duplicateFound = statesSeen.ContainsKey(state = new string(currState)))
                        minutes = (minutes - minute) % (minute - statesSeen[state]) + minute;
                    else
                        statesSeen.Add(state, minute);
                }
                for (int index = 50; index < grid.Length - 50; index++)
                {
                    List<int> offsets;
                    switch (index % 50) //check if on left or right edge of grid
                    {
                        case 0:
                            offsets = new List<int> { -50, -49, 1, 50, 51 };
                            break;
                        case 49:
                            offsets = new List<int> { -51, -50, -1, 49, 50 };
                            break;
                        default:
                            offsets = new List<int> { -51, -50, -49, -1, 1, 49, 50, 51 };
                            break;
                    }

                    switch (currState[index])
                    {
                        case '.':
                            int trees = 0;
                            foreach (int offset in offsets)
                            {
                                if (currState[index + offset] == '|')
                                {
                                    if (++trees >= 3)
                                    {
                                        grid[index] = '|';
                                        break;
                                    }
                                }
                            }
                            break;
                        case '|':
                            int lumberyards = 0;
                            foreach (int offset in offsets)
                            {
                                if (currState[index + offset] == '#')
                                {
                                    if (++lumberyards >= 3)
                                    {
                                        grid[index] = '#';
                                        break;
                                    }
                                }
                            }
                            break;
                        case '#':
                            bool anyTrees = false;
                            bool anyLumberyards = false;
                            foreach (int offset in offsets)
                            {
                                if (currState[index + offset] == '|')
                                    anyTrees = true;
                                else if (currState[index + offset] == '#')
                                    anyLumberyards = true;
                                if(anyTrees && anyLumberyards)
                                    break;
                            }
                            if (anyTrees && anyLumberyards)
                                break;
                            grid[index] = '.';
                            break;
                    }
                }
                if (minute == 9)
                    Console.WriteLine("Minute 10 Resource Value: " + (grid.Skip(50).Take(2500).Where(x => x == '|').Count() * grid.Skip(50).Take(2500).Where(x => x == '#').Count()));
            }
            Console.WriteLine("Final Resource Value: " + (grid.Skip(50).Take(2500).Where(x => x == '|').Count() * grid.Skip(50).Take(2500).Where(x => x == '#').Count()));
        }
    }
}
