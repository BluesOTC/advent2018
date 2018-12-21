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
            /*int length;
            do
            {
                length = regexMap.Length;
                foreach (string match in backtracks)
                    regexMap.Replace(match, "");
            } while (regexMap.Length < length);
            int index = 1;
            Console.WriteLine(traverse(ref index));*/

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

        //switch to stack: can deal with dead-ends (loops!) better

        /*static int traverse(ref int index)
        {
            Stack<char> currPath = new Stack<char>();
            int distance = 0;
            for (; index < regexMap.Length; index++)
            {
                if (currPath.Count > 0 && (regexMap[index] == 'W' && currPath.Peek() == 'E' || regexMap[index] == 'E' && currPath.Peek() == 'W' || regexMap[index] == 'S' && currPath.Peek() == 'N' || regexMap[index] == 'N' && currPath.Peek() == 'S'))
                {
                    currPath.Pop();
                    distance--;
                }
                else
                    currPath.Push(regexMap[index]);
                if (regexMap[index] == '$')
                    return distance;
                if (regexMap[index] == ')' || regexMap[index] == '|')
                {
                    index++;
                    return distance;
                }
                if (regexMap[index] == '(')
                {
                    index++;
                    return distance + traverseJunction(ref index);
                }
                distance++;
            }
            return 0; //for final branch
        }

        static int traverseJunction(ref int index)
        {
            int branch1 = traverse(ref index);
            int branch2 = traverse(ref index);
            if(branch1 > 0 && branch2 > 0)
                return Math.Max(branch1, branch2) + traverse(ref index);
            return Math.Max(Math.Max(branch1, branch2), traverse(ref index));
        }*/
    }
}
