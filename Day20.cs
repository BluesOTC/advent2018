using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Advent
{
    class Day20
    {
        static StringBuilder regexMap;

        public static void Run()
        {
            Console.WriteLine("\nDay 20");
            List<string> backtracks = new List<string> { "NS", "SN", "EW", "WE" };
            using (StreamReader reader = new StreamReader("input/input20.txt"))
                regexMap = new StringBuilder(reader.ReadLine());

            HashSet<Coordinate> coordinates = new HashSet<Coordinate>();
            Coordinate currCoordinate = new Coordinate(0, 0);
            coordinates.Add(currCoordinate);
            Stack<Coordinate> junctions = new Stack<Coordinate>();
            for(int index = 0; index < regexMap.Length; index++)
            {
                switch(regexMap[index])
                {
                    case 'N':
                        coordinates.Add(currCoordinate = currCoordinate.AddVector(0, -1));
                        break;
                    case 'S':
                        coordinates.Add(currCoordinate = currCoordinate.AddVector(0, 1));
                        break;
                    case 'E':
                        coordinates.Add(currCoordinate = currCoordinate.AddVector(1, 0));
                        break;
                    case 'W':
                        coordinates.Add(currCoordinate = currCoordinate.AddVector(-1, 0));
                        break;
                    case '(':
                        junctions.Push(currCoordinate);
                        break;
                    case '|':
                        currCoordinate = junctions.Peek();
                        break;
                    case ')':
                        currCoordinate = junctions.Pop();
                        break;
                }
            }

            List<Coordinate> directions = new List<Coordinate> { new Coordinate(0, -1), new Coordinate(-1, 0), new Coordinate(1, 0), new Coordinate(0, 1) };
            int depth = 0;
            HashSet<Coordinate> currentRooms = new HashSet<Coordinate>();
            HashSet<Coordinate> visitedRooms = new HashSet<Coordinate>();
            currentRooms.Add(new Coordinate(0, 0));
            visitedRooms.Add(new Coordinate(0, 0));
            while (visitedRooms.Count < coordinates.Count)
            {
                HashSet<Coordinate> nextRooms = new HashSet<Coordinate>();
                foreach (Coordinate source in currentRooms)
                {
                    foreach (Coordinate vector in directions)
                    {
                        Coordinate nextRoom = source.AddVector(vector);
                        if(coordinates.Contains(nextRoom))
                        {
                            nextRooms.Add(nextRoom);
                            visitedRooms.Add(nextRoom);
                        }
                    }
                }
                currentRooms = nextRooms;
                depth++;
            }
            Console.WriteLine("Distance traveled: " + depth);
        }
    }
}
