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
            using (StreamReader reader = new StreamReader("input9.txt"))
                splitLine = reader.ReadLine().Split(' ');
            int players = Int32.Parse(splitLine[0]);
            int lastMarble = Int32.Parse(splitLine[6]);
            lastMarble -= lastMarble % 23;

            LinkedList<int> marbles = new LinkedList<int>();
            long[] scores = new long[players];
            LinkedListNode<int> currentNode = marbles.AddFirst(0);
            for (int marble = 1; marble <= lastMarble * 100; marble++)
            {
                if (marble % 23 == 0)
                {
                    for(int i = 0; i < 6; i++)
                        currentNode = currentNode.Previous ?? marbles.Last;
                    scores[marble % players] += (currentNode.Previous ?? marbles.Last).Value + marble;
                    marbles.Remove(currentNode.Previous ?? marbles.Last);
                }
                else
                    currentNode = marbles.AddAfter(currentNode.Next ?? marbles.First, marble);
                if (marble == lastMarble)
                    Console.WriteLine("Part 1 Score: " + scores.Max());
            }
            Console.WriteLine("High Score: " + scores.Max());
        }
    }
}
