using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent
{
    class Day10
    {
        public static void Run(List<string> input)
        {
            List<List<Coordinate>> points = new List<List<Coordinate>>();
            int ymax = Int32.MinValue;
            int ymaxvel = 0;
            int ymin = Int32.MaxValue;
            int yminvel = 0;

            foreach (string line in input)
            {
                int xpos = Int32.Parse(line.Substring(10, 6).Trim());
                int ypos = Int32.Parse(line.Substring(18, 6).Trim());
                int xvel = Int32.Parse(line.Substring(36, 2).Trim());
                int yvel = Int32.Parse(line.Substring(40, 2).Trim());
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

            int maxDistance = 236; //single-line limit on my console :P
            int minSteps = (ymax - ymin - maxDistance / 2) / (yminvel - ymaxvel);
            int additionalSteps = (ymax - ymin + maxDistance / 2) / (yminvel - ymaxvel) - minSteps;

            int xmin = Int32.MaxValue;
            ymin = Int32.MaxValue;
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
                    output.Add(Enumerable.Repeat('.', 236).ToArray());
                foreach (List<Coordinate> point in points)
                {
                    output[point[0].x][point[0].y] = '#';
                    point[0].x += point[1].x;
                    point[0].y += point[1].y;
                }
                for(int index = 0; index < output.Count; index++)
                {
                    Console.WriteLine(new string(output[index]));
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
