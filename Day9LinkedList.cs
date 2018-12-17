using System;
using System.Collections.Generic;
using System.IO;

namespace Advent
{
    class Day9LinkedList
    {
        public static void Run()
        {
            Console.WriteLine();
            Console.WriteLine("Day 9");

            string[] splitLine; 
            using (StreamReader reader = new StreamReader("input9.txt"))
            {
                splitLine = reader.ReadLine().Split(' ');
            }
            int players = Int32.Parse(splitLine[0]);
            int lastMarble = Int32.Parse(splitLine[6]);
            lastMarble -= lastMarble % 23;

            LinkedList<int> marbles = new LinkedList<int>();
            long[] scores = new long[players];
            marbles.AddFirst(0);
            LinkedListNode<int> currentNode = marbles.First;
            for (int marble = 1; marble <= lastMarble * 100; marble++)
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
                if (marble == lastMarble)
                {
                    long part1Score = 0;
                    foreach (long score in scores)
                    {
                        if (score > part1Score)
                            part1Score = score;
                    }
                    Console.WriteLine("Part 1 Score: " + part1Score);
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
