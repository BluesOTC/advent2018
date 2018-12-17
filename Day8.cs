using System;
using System.IO;
using System.Linq;

namespace Advent
{
    class Day8
    {
        public static void Run()
        {
            Console.WriteLine();
            Console.WriteLine("Day 8");

            int[] splitLine;
            using (StreamReader reader = new StreamReader("input8.txt"))
            {
                splitLine = reader.ReadLine().Split(' ').Select(x => Int32.Parse(x)).ToArray();
            }
            int index = 0;
            Console.WriteLine("Sum: " + processChild(splitLine, ref index));
        }

        static int processChild(int[] input, ref int index)
        {
            int children = input[index];
            index++;
            int metadataEntries = input[index];
            index++;
            int sum = 0;
            int[] childValues = new int[children];
            if (children > 0)
            {
                for (int i = 0; i < children; i++)
                    childValues[i] = processChild(input, ref index);
                for (int i = 0; i < metadataEntries; i++)
                {
                    if (input[index] - 1 < childValues.Count())
                        sum += childValues[input[index] - 1];
                    index++;
                }
            }
            else
            {
                for (int i = 0; i < metadataEntries; i++)
                {
                    sum += input[index];
                    index++;
                }
            }
            return sum;
        }
    }
}
