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
            //only need to check half of the states; if arrangement is not found, must be other state
            //array length? can maybe initialize to +21 or so, then only iterate on left-most if they become active (since things only activate for the first time if both on right are active)
            string state = input[0].Split(' ')[2];
            List<string> activations = new List<string>();

            for(int index = 2; index < input.Count; index++)
            {
                if (input[index][9] == '#')
                    activations.Add(input[index].Substring(0, 5));
            }

            StringBuilder currGeneration = new StringBuilder();
            currGeneration.Append(new string('.', 21));
            currGeneration.Append(state);
            currGeneration.Append(new string('.', 210));
            Console.WriteLine(currGeneration.ToString().Trim('.'));

            HashSet<string> patternsSeen = new HashSet<string>();
            for(int generation = 0; generation < 500; generation++) //edit generation number after first run to see how plant pattern shifts
            {
                char[] currState = new char[currGeneration.Length];
                currGeneration.CopyTo(0, currState, 0, currGeneration.Length);
                StringBuilder nextGeneration = new StringBuilder(new string(currState));
                for (int index = 2; index < currGeneration.Length - 3; index++)
                {
                    if (activations.Contains(currGeneration.ToString().Substring(index - 2, 5)))
                        nextGeneration[index] = '#';
                    else
                        nextGeneration[index] = '.';
                }
                currGeneration = nextGeneration;
                if (patternsSeen.Contains(currGeneration.ToString().Trim('.')))
                {
                    Console.WriteLine(String.Format("Repeat Pattern found at generation {0}", generation));
                    Console.WriteLine(currGeneration.ToString().Trim('.'));
                    Console.WriteLine(String.Format("First plant found at pot #{0}", currGeneration.ToString().IndexOf('#')));
                    break;
                }
                else
                    patternsSeen.Add(currGeneration.ToString().Trim('.'));
                Console.WriteLine(currGeneration.ToString().Trim('.'));
            }

            int total = 0;
            for (int index = 0; index < currGeneration.Length; index++)
                total += (currGeneration[index] == '#' ? 1 : 0) * (index - 21);
            Console.WriteLine("Sum Product: " + total);
        }
    }
}
