using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Advent
{
    class Day10
    {
        public static void Run()
        {
            Console.WriteLine("\nDay 10");

            List<string> input = new List<string>();
            using (StreamReader reader = new StreamReader("input/input10.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    input.Add(line);
            }
            List<List<Coordinate>> points = new List<List<Coordinate>>();
            int ymax = int.MinValue;
            int ymaxvel = 0;
            int ymin = int.MaxValue;
            int yminvel = 0;

            foreach (string line in input)
            {
                int xpos = int.Parse(line.Substring(10, 6).Trim());
                int ypos = int.Parse(line.Substring(18, 6).Trim());
                int xvel = int.Parse(line.Substring(36, 2).Trim());
                int yvel = int.Parse(line.Substring(40, 2).Trim());
                points.Add(new List<Coordinate> { new Coordinate(xpos, ypos), new Coordinate(xvel, yvel) });
                if (ypos > ymax)
                {
                    ymax = ypos;
                    ymaxvel = yvel;
                }
                if (ypos < ymin)
                {
                    ymin = ypos;
                    yminvel = yvel;
                }
            }

            int maxDistance = 18;
            int minSteps = (ymax - ymin - maxDistance / 2) / (yminvel - ymaxvel);
            int additionalSteps = (ymax - ymin + maxDistance / 2) / (yminvel - ymaxvel) - minSteps;

            int xmin = int.MaxValue;
            ymin = int.MaxValue;
            foreach (List<Coordinate> point in points)
            {
                point[0].x += point[1].x * minSteps;
                point[0].y += point[1].y * minSteps;
                if (point[0].x < xmin)
                    xmin = point[0].x;
                if (point[0].y < ymin)
                    ymin = point[0].y;
            }


            foreach (List<Coordinate> point in points)
            {
                point[0].x -= xmin;
                point[0].y -= ymin;
            }

            for (int i = 0; i < additionalSteps; i++)
            {
                Console.WriteLine("Seconds passed: " + (minSteps + i));
                List<char[]> output = new List<char[]>();
                for(int index = 0; index < 200; index++)
                    output.Add(Enumerable.Repeat('.', maxDistance).ToArray());
                foreach (List<Coordinate> point in points)
                {
                    output[point[0].x][point[0].y] = '#';
                    point[0].x += point[1].x;
                    point[0].y += point[1].y;
                }
                for(int index = 0; index < output.Count; index++)
                {
                    if (!output[index].All(x => x == '.'))
                        Console.WriteLine(new string(output[index]).Substring(0, maxDistance));
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
