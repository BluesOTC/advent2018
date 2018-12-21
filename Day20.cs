using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Advent
{
    class Day20
    {
        public class Room : Coordinate
        {
            public List<Room> connections;

            public Room(int x, int y) : base(x, y)
            {
                connections = new List<Room>();
            }

            public Room AddNeighbor(int x, int y)
            {
                Room room = new Room(this.x + x, this.y + y);
                connections.Add(room);
                return room;
            }

            public override bool Equals(object other)
            {
                return base.Equals(other);
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        public static void Run()
        {
            Console.WriteLine("\nDay 20");
            string regexMap;
            using (StreamReader reader = new StreamReader("input/input20.txt"))
                regexMap = reader.ReadLine();

            HashSet<Room> rooms = new HashSet<Room>();
            Room currRoom = new Room(0, 0);
            rooms.Add(currRoom);
            Stack<Room> junctions = new Stack<Room>();
            for(int index = 0; index < regexMap.Length; index++)
            {
                switch(regexMap[index])
                {
                    case 'N':
                        rooms.Add(currRoom = currRoom.AddNeighbor(0, -1));
                        break;
                    case 'S':
                        rooms.Add(currRoom = currRoom.AddNeighbor(0, 1));
                        break;
                    case 'E':
                        rooms.Add(currRoom = currRoom.AddNeighbor(1, 0));
                        break;
                    case 'W':
                        rooms.Add(currRoom = currRoom.AddNeighbor(-1, 0));
                        break;
                    case '(':
                        junctions.Push(currRoom);
                        break;
                    case '|':
                        currRoom = junctions.Peek();
                        break;
                    case ')':
                        currRoom = junctions.Pop();
                        break;
                }
            }

            int depth = 0;
            HashSet<Room> currentRooms = new HashSet<Room>();
            HashSet<Room> visitedRooms = new HashSet<Room>();
            currentRooms.Add(rooms.First(x => x.x == 0 && x.y == 0));
            visitedRooms.Add(currentRooms.Single());
            while (visitedRooms.Count < rooms.Count)
            {
                 HashSet<Room> nextRooms = new HashSet<Room>();
                foreach (Room source in currentRooms)
                {
                    foreach (Room neighbor in source.connections)
                    {
                        nextRooms.Add(neighbor);
                        visitedRooms.Add(neighbor);
                    }
                }
                currentRooms = nextRooms;
                depth++;
                if (depth == 999)
                    Console.WriteLine("Part 2 - Rooms after 1000 doors: " + (rooms.Count - visitedRooms.Count));
            }
            Console.WriteLine("Part 1 - Distance traveled: " + depth);
        }
    }
}
