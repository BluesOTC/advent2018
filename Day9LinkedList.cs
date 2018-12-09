using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent
{
    class Day9LinkedList
    {
        public static void Run(List<string> input)
        {
            string[] splitLine = input[0].Split(' ');
            int players = Int32.Parse(splitLine[0]);
            int lastMarble = Int32.Parse(splitLine[6]) * 100;
            lastMarble -= lastMarble % 23;

            LinkedList<int> marbles = new LinkedList<int>();
            long[] scores = new long[players];
            marbles.AddFirst(0);
            LinkedListNode<int> currentNode = marbles.First;
            for (int marble = 1; marble <= lastMarble; marble++)
            {
                if (marble % 23 == 0)
                {
                    for(int i = 0; i < 7; i++)
                    {
                        currentNode = currentNode.Previous;
                        if (currentNode == null)
                            currentNode = marbles.Last;
                    }
                    scores[marble % players] += currentNode.Value + marble;
                    currentNode = currentNode.Next;
                    if (currentNode == null)
                    {
                        currentNode = marbles.First;
                        marbles.RemoveLast();
                    }
                    else
                        marbles.Remove(currentNode.Previous);
                }
                else
                {
                    if (currentNode.Next == null)
                    {
                        currentNode = marbles.First;
                        marbles.AddAfter(currentNode, marble);
                        currentNode = currentNode.Next;
                    }
                    else if (currentNode.Next.Next == null)
                    {
                        marbles.AddLast(marble);
                        currentNode = marbles.Last;
                    }
                    else
                    {
                        currentNode = currentNode.Next;
                        marbles.AddAfter(currentNode, marble);
                        currentNode = currentNode.Next;
                    }
                }
            }

            long highScore = 0;
            foreach (long score in scores)
            {
                if (score > highScore)
                    highScore = score;
            }
            Console.WriteLine("High Score: " + highScore);
        }
    }
}
