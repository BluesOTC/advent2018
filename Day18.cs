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

            char[] grid = new char[2500]; //1D array makes comparisons and linq operations easier but unintuitive
            using (StreamReader reader = new StreamReader("input/input18.txt"))
            {
                int row = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    line.CopyTo(0, grid, 50 * row, 50);
                    row++;
                }
            }

            bool duplicateFound = false;
            int minutes = 1000000000;
            List<int> leftOffsets = new List<int> { -51, -1, 49 };
            List<int> rightOffsets = new List<int> { -49, 1, 51 };
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
                for (int index = 0; index < grid.Length; index++)
                {
                    List<int> offsets = new List<int> { -50, 50 };
                    if (index % 50 != 0)
                        offsets.AddRange(leftOffsets);
                    if (index % 50 != 49)
                        offsets.AddRange(rightOffsets);
                    List<char> surroundings = new List<char>();
                    foreach (int offset in offsets)
                    {
                        if (index + offset < 0 || index + offset >= grid.Length)
                            continue;                        
                        surroundings.Add(currState[index + offset]);
                    }
                    switch (currState[index])
                    {
                        case '.':
                            if (surroundings.Where(o => o == '|').Count() >= 3)
                                grid[index] = '|';
                            break;
                        case '|':
                            if (surroundings.Where(o => o == '#').Count() >= 3)
                                grid[index] = '#';
                            break;
                        case '#':
                            if (!surroundings.Any(o => o == '|') || !surroundings.Any(o => o == '#'))
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
