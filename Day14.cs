using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent
{
    class Day14
    {
        public static void Run(List<string> unnecessary)
        {
            List<int> scoreboard = new List<int> { 3, 7 };
            int elf1 = 0;
            int elf2 = 1;
            int recipes = 2;
            int index = 0;
            int[] input = new int[] { 6, 3, 3, 6, 0, 1 };
            //int[] input = new int[] { 0, 1, 2, 4, 5 };

            while (true /*recipes <= 633611*/)
            {
                int sum = scoreboard[elf1] + scoreboard[elf2];
                if (sum > 9)
                {
                    scoreboard.Add(1);
                    recipes += 2;
                    scoreboard.Add(sum % 10);
                }
                else
                {
                    scoreboard.Add(sum);
                    recipes++;
                }
                if (sum == input[index])
                {
                    index++;
                    if (index >= input.Length)
                        break;
                }
                else if(sum > 9)
                {
                    if (input[index] == 1)
                    {
                        index++;
                        if (index >= input.Length)
                        {
                            index++;
                            break;
                        }
                        if (input[index] == sum % 10)
                        {
                            index++;
                            if (index >= input.Length)
                                break;
                        }
                        else
                        {
                            index = 0;
                            if (input[index] == sum % 10)
                            {
                                index++;
                                if (index >= input.Length)
                                    break;
                            }
                        }
                    }
                    else
                    {
                        index = 0;
                        if (input[index] == sum % 10)
                        {
                            index++;
                            if (index >= input.Length)
                                break;
                        }
                    }
                }
                else
                    index = 0;
                elf1 = (elf1 + scoreboard[elf1] + 1) % scoreboard.Count;
                elf2 = (elf2 + scoreboard[elf2] + 1) % scoreboard.Count;
            }

            //foreach (int i in scoreboard.GetRange(633601, 10)) //for part 1
            //    Console.Write(i);
            Console.WriteLine(recipes - index);
        }
    }
}
