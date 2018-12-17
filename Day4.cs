using System;
using System.Collections.Generic;
using System.IO;

namespace Advent
{
    class Day4
    {
        public static void Run()
        {
            Console.WriteLine();
            Console.WriteLine("Day 4");

            List<string> input = new List<string>();
            using (StreamReader reader = new StreamReader("input4.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    input.Add(line);
            }

            input.Sort();
            Dictionary<int, int[]> sleepTable = new Dictionary<int, int[]>();
            int currID = -1;
            int sleepTime = -1;
            //index 15 for minutes, 25 for ID/s/p
            foreach (string line in input)
            {
                char type = line[26];
                if (type == 's')
                {
                    sleepTime = Int32.Parse(line.Substring(15, 2));
                }
                else if (type == 'p')
                {
                    int wakeTime = Int32.Parse(line.Substring(15, 2));
                    for (int minute = sleepTime; minute < wakeTime; minute++)
                    {
                        sleepTable[currID][minute]++;
                        sleepTable[currID][60]++;
                    }
                }
                else
                {
                    currID = Int32.Parse(line.Substring(26, 4).Trim());
                    if (!sleepTable.ContainsKey(currID))
                        sleepTable.Add(currID, new int[61]);
                }
            }

            //Day 4-1
            int iBestID = -1;
            int iMostSleep = -1;
            foreach (int ID in sleepTable.Keys)
            {
                if (sleepTable[ID][60] > iMostSleep)
                {
                    iMostSleep = sleepTable[ID][60];
                    iBestID = ID;
                }
            }

            int iBestMinute = -1;
            int iHighestCount = -1;
            for (int minute = 0; minute < 60; minute++)
            {
                if (sleepTable[iBestID][minute] > iHighestCount)
                {
                    iHighestCount = sleepTable[iBestID][minute];
                    iBestMinute = minute;
                }
            }

            //Day 4-2
            iBestMinute = -1;
            iHighestCount = -1;
            iBestID = -1;
            foreach (int ID in sleepTable.Keys)
            {
                for (int minute = 0; minute < 60; minute++)
                {
                    if (sleepTable[ID][minute] > iHighestCount)
                    {
                        iHighestCount = sleepTable[ID][minute];
                        iBestMinute = minute;
                        iBestID = ID;
                    }
                }
            }

            Console.WriteLine("ID x Minute: " + iBestID * iBestMinute);
        }
    }
}
