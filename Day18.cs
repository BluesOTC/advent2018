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

            char[] grid = new char[2600]; //1D array makes comparisons and linq operations easier but unintuitive
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
                List<char> currState = grid.Select(x => x).ToList();
                string state;
                if (!duplicateFound)
                {
                    if (statesSeen.ContainsKey(state = new string(currState.ToArray())))
                    {
                        duplicateFound = true;
                        minutes = (minutes - minute) % (minute - statesSeen[state]) + minute;
                    }
                    else
                        statesSeen.Add(state, minute);
                }
                for (int index = 50; index < grid.Length - 50; index++)
                {
                    List<int> offsets;
                    if (index % 50 == 0)
                        offsets = new List<int> { -50, -49, 1, 50, 51 };
                    else if (index % 50 == 49)
                        offsets = new List<int> { -51, -50, -1, 49, 50 };
                    else
                        offsets = new List<int> { -51, -50, -49, -1, 1, 49, 50, 51 };

                    switch (currState[index])
                    {
                        case '.':
                            int trees = 0;
                            foreach (int offset in offsets)
                            {
                                if (currState[index + offset] == '|')
                                {
                                    trees++;
                                    if (trees >= 3)
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
                                    lumberyards++;
                                    if (lumberyards >= 3)
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
                    Console.WriteLine("Minute 10 Resource Value: " + (grid.Where(x => x == '|').Count() * grid.Where(x => x == '#').Count()));
            }
            Console.WriteLine("Final Resource Value: " + (grid.Where(x => x == '|').Count() * grid.Where(x => x == '#').Count()));
        }
    }
}
