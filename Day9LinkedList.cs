using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Advent
{
    class Day9LinkedList
    {
        public static void Run()
        {
            Console.WriteLine("\nDay 9");

            string[] splitLine; 
            using (StreamReader reader = new StreamReader("input/input9.txt"))
                splitLine = reader.ReadLine().Split(' ');
            int players = int.Parse(splitLine[0]);
            int lastMarble = int.Parse(splitLine[6]);
            lastMarble -= lastMarble % 23;
            int part2FinalMarble = lastMarble * 100;

            LinkedList<int> marbles = new LinkedList<int>();
            long[] scores = new long[players];
            LinkedListNode<int> currentNode = marbles.AddFirst(0);
            for (int marble = 1; marble <= part2FinalMarble; marble++)
            {
                if (marble % 23 == 0)
                {
                    for(int i = 0; i < 6; i++)
                        currentNode = currentNode.Previous ?? marbles.Last;
                    scores[marble % players] += (currentNode.Previous ?? marbles.Last).Value + marble;
                    marbles.Remove(currentNode.Previous ?? marbles.Last);
                    if (marble == lastMarble)
                        Console.WriteLine("Part 1 Score: " + scores.Max());
                }
                else
                    currentNode = marbles.AddAfter(currentNode.Next ?? marbles.First, marble);
            }
            Console.WriteLine("High Score: " + scores.Max());
        }
    }
}
