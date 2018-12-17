using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Advent
{
    class Day12
    {
        public static void Run()
        {
            Console.WriteLine();
            Console.WriteLine("Day 12");

            List<string> input = new List<string>();
            using (StreamReader reader = new StreamReader("input12.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    input.Add(line);
            }

            int generations = 20000;
            string state = input[0].Split(' ')[2];
            List<string> activations = new List<string>();

            for (int index = 2; index < input.Count; index++)
            {
                if (input[index][9] == '#')
                    activations.Add(input[index].Substring(0, 5));
            }

            StringBuilder currGeneration = new StringBuilder("..");
            currGeneration.Append(state);
            currGeneration.Append(new string('.', 2));
            int offset = 2;

            bool shouldBreak = false;
            int plantShift = 0;
            int generation = 0;
            HashSet<string> patternsSeen = new HashSet<string>();
            for (; generation < generations; generation++) //edit generation number after first run to see how plant pattern shifts
            {
                char[] currState = new char[currGeneration.Length];
                currGeneration.CopyTo(0, currState, 0, currGeneration.Length);
                StringBuilder nextGeneration = new StringBuilder(new string(currState));
                for (int index = 2; index < nextGeneration.Length - 2; index++)
                {
                    if (activations.Contains(currGeneration.ToString().Substring(index - 2, 5)))
                        nextGeneration[index] = '#';
                    else
                        nextGeneration[index] = '.';
                }
                if (nextGeneration.ToString().IndexOf('#') <= 2)
                {
                    nextGeneration.Insert(0, "..");
                    offset += 2;
                }
                if (nextGeneration.ToString().LastIndexOf('#') >= nextGeneration.Length - 3)
                    nextGeneration.Append("..");
                currGeneration = nextGeneration;
                if (patternsSeen.Contains(currGeneration.ToString().Trim('.')))
                {
                    //Console.WriteLine(String.Format("Repeat Pattern found at generation {0}", generation));
                    //Console.WriteLine(String.Format("First plant found at pot #{0}", currGeneration.ToString().IndexOf('#')));
                    if (shouldBreak)
                        break;
                    shouldBreak = true;
                    plantShift = currGeneration.ToString().IndexOf('#');
                }
                else
                    patternsSeen.Add(currGeneration.ToString().Trim('.'));
                if (generation == 20)
                {
                    int part1Total = 0;
                    int part1Plants = 0;
                    for (int index = 0; index < currGeneration.Length; index++)
                    {
                        if (currGeneration[index] == '#')
                        {
                            part1Plants++;
                            part1Total += (index - offset);
                        }
                    }
                    Console.WriteLine("Generation 20 Sum Product: " + part1Total + " from " + part1Plants + " plants");
                }
            }

            long total = 0;
            int plants = 0;
            plantShift = currGeneration.ToString().IndexOf('#') - plantShift;
            for (int index = 0; index < currGeneration.Length; index++)
            {
                if (currGeneration[index] == '#')
                {
                    plants++;
                    total += (index - offset);
                }
            }
            //Console.WriteLine("Repeat Generation Sum Product: " + total + " from " + plants + " plants");
            long finalTotal = total + (long)plantShift * (50000000000L - (long)generation) * (long)plants;
            Console.WriteLine("Generation 50b: Sum Product: " + finalTotal + " from " + plants + " plants");
        }
    }
}
