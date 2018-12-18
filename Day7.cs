﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Advent
{
    class Day7
    {
        public static void Run()
        {
            Console.WriteLine("\nDay 7");

            List<string> input = new List<string>();
            using (StreamReader reader = new StreamReader("input/input7.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    input.Add(line);
            }

            Dictionary<char, List<char>> stepTree = new Dictionary<char, List<char>>();
            foreach (string line in input)
            {
                string[] split = line.Split(' ');
                char prereq = split[1][0];
                char subsequent = split[7][0];
                if (!stepTree.ContainsKey(prereq))
                    stepTree.Add(prereq, new List<char>());
                if (!stepTree.ContainsKey(subsequent))
                    stepTree.Add(subsequent, new List<char> { prereq });
                else
                    stepTree[subsequent].Add(prereq);
            }

            //TODO: redo part 1 and add print

            Dictionary<char, int> elfAssignments = new Dictionary<char, int>();
            List<char> remainingSteps = stepTree.Keys.ToList();
            remainingSteps.Sort();
            List<char> completedSteps = new List<char>();
            int time = 0;
            fillAssignments(ref elfAssignments, completedSteps, ref remainingSteps, time, stepTree);
            while (remainingSteps.Count > 0)
            {
                time++;
                List<int> completionTimes = elfAssignments.Values.ToList();
                foreach (int t in completionTimes)
                {
                    if (t <= time)
                    {
                        char key = '\0';
                        foreach (char c in elfAssignments.Keys)
                        {
                            if (elfAssignments[c] == t)
                            {
                                key = c;
                                break;
                            }
                        }
                        completedSteps.Add(key);
                        elfAssignments.Remove(key);
                        fillAssignments(ref elfAssignments, completedSteps, ref remainingSteps, time, stepTree);
                    }
                }
            }
            foreach (char c in elfAssignments.Keys)
            {
                if (elfAssignments[c] > time)
                    time = elfAssignments[c];
            }
            Console.WriteLine("\nFinish time: " + time);
        }

        static void fillAssignments(ref Dictionary<char, int> elfAssignments, List<char> completedSteps, ref List<char> remainingSteps, int time, Dictionary<char, List<char>> stepTree)
        {
            bool assignmentFound = true;
            while (assignmentFound && elfAssignments.Count < 5)
                assignmentFound = findNextAssignment(ref elfAssignments, completedSteps, ref remainingSteps, time, stepTree);
        }

        static bool findNextAssignment(ref Dictionary<char, int> elfAssignments, List<char> completedSteps, ref List<char> remainingSteps, int time, Dictionary<char, List<char>> stepTree)
        {
            foreach (char step in remainingSteps)
            {
                bool prereqsComplete = true;
                foreach (char prereq in stepTree[step])
                {
                    if (!completedSteps.Contains(prereq))
                    {
                        prereqsComplete = false;
                        break;
                    }
                }
                if (prereqsComplete)
                {
                    //Console.Write(step);
                    remainingSteps.Remove(step);
                    elfAssignments.Add(step, time + step - 4);
                    return true;
                }
            }
            return false;
        }
    }
}
