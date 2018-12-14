using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent
{
    class Day12
    {
        public static void Run(List<string> input)
        {
            int generations = 20000;
            DateTime startTime = DateTime.Now;
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

            HashSet<string> patternsSeen = new HashSet<string>();
            for (int generation = 0; generation < generations; generation++) //edit generation number after first run to see how plant pattern shifts
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
                /*if (patternsSeen.Contains(currGeneration.ToString().Trim('.')))
                {
                    Console.WriteLine(String.Format("Repeat Pattern found at generation {0}", generation));
                    Console.WriteLine(String.Format("First plant found at pot #{0}", currGeneration.ToString().IndexOf('#')));
                    //break;
                }
                else
                    patternsSeen.Add(currGeneration.ToString().Trim('.'));*/
            }

            int total = 0;
            int plants = 0;
            for (int index = 0; index < currGeneration.Length; index++)
            {
                plants++;
                total += (currGeneration[index] == '#' ? 1 : 0) * (index - offset);
            } 
            Console.WriteLine("Sum Product: " + total + " from " + plants + " plants");
            Console.WriteLine(DateTime.Now - startTime);
        }
    }
}
