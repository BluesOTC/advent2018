using System;
using System.IO;
using System.Linq;

namespace Advent
{
    class Day8
    {
        public static void Run()
        {
            Console.WriteLine("\nDay 8");

            int[] splitLine;
            using (StreamReader reader = new StreamReader("input8.txt"))
                splitLine = reader.ReadLine().Split(' ').Select(x => Int32.Parse(x)).ToArray();

            int index = 0;
            Console.WriteLine("Part 1 Sum: " + processChildPart1(splitLine, ref index));
            index = 0;
            Console.WriteLine("Part 2 Sum: " + processChildPart2(splitLine, ref index));
        }

        static int processChildPart2(int[] input, ref int index)
        {
            int children = input[index];
            int metadataEntries = input[index + 1];
            index += 2;
            int[] childValues = new int[children];
            if (children > 0)
            {
                for (int i = 0; i < children; i++)
                    childValues[i] = processChildPart2(input, ref index);
                int sum = 0;
                for (int i = 0; i < metadataEntries; i++)
                {
                    if (input[index] - 1 < childValues.Count())
                        sum += childValues[input[index] - 1];
                    index++;
                }
                return sum;
            }
            else
            {
                index += metadataEntries;
                return input.Skip(index - metadataEntries).Take(metadataEntries).Sum();
            }
        }

        static int processChildPart1(int[] input, ref int index)
        {
            int children = input[index];
            int metadataEntries = input[index + 1];
            index += 2;
            int sum = 0;
            for (int i = 0; i < children; i++)
                sum += processChildPart1(input, ref index);
            index += metadataEntries;
            return sum + input.Skip(index - metadataEntries).Take(metadataEntries).Sum();
        }
    }
}
