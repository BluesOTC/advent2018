using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent
{
    class Day9Naive
    {
        public static void Run(List<string> input)
        {
            string[] splitLine = input[0].Split(' ');
            int players = Int32.Parse(splitLine[0]);
            int lastMarble = Int32.Parse(splitLine[6]) * 100;
            lastMarble -= lastMarble % 23;

            List<int> marbles = new List<int> { 0, 1 };
            int[] scores = new int[players];
            int currentIndex = 1;
            for(int marble = 2; marble <= lastMarble; marble++)
            {
                if (marble % 23 == 0)
                {
                    currentIndex -= 7;
                    if (currentIndex < 0)
                        currentIndex += marbles.Count;
                    scores[marble % players] += marbles[currentIndex] + marble;
                    marbles.RemoveAt(currentIndex);
                }
                else
                {
                    if ((currentIndex + 2) % marbles.Count == 0)
                    {
                        currentIndex = marbles.Count;
                        marbles.Add(marble);
                    }
                    else
                    {
                        currentIndex = Math.Max((currentIndex + 1) % marbles.Count, (currentIndex + 2) % marbles.Count);
                        marbles.Insert(currentIndex, marble);
                    }
                }
            }

            int highScore = 0;
            foreach(int score in scores)
            {
                if (score > highScore)
                    highScore = score;
            }
            Console.WriteLine("High Score: " + highScore);
        }
    }
}
