/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent
{
    class NodeAStar : Coordinate, IComparable<NodeAStar>
    {
        public int distFromStart;
        NodeAStar parent;

        public NodeAStar(int x, int y, int distFromParent, NodeAStar parent) : base(x, y)
        {
            this.distFromStart = distFromParent;
            this.parent = parent;
        }

        int IComparable<NodeAStar>.CompareTo(NodeAStar other)
        {
            return 0; //
            //return f - other.f;
        }
    }

    class EntityAStar : Coordinate
    {
        List<Coordinate> directions = new List<Coordinate> { new Coordinate(0, -1), new Coordinate(1, 0), new Coordinate(0, 1), new Coordinate(-1, 0) }; //north, east, south, west

        int hp;
        int attack;
        List<char[]> grid;
        EntityType type;
        Stack<Coordinate> currPath;
        List<NodeAStar> closedNodes;
        List<NodeAStar> openNodes;

        public EntityAStar(int x, int y, EntityType type, ref List<char[]> grid) : base(x, y)
        {
            this.type = type;
            hp = 200;
            attack = 3;
            currPath = new Stack<Coordinate>();
            this.grid = grid;
        }

        public void findNearestTarget(List<Entity> targets) //needs to find nearest open space adjacent to target, not nearest target
        {
            int minDistance = Int32.MaxValue;

            foreach (Entity target in targets)
            {
                foreach (Coordinate candidate in getValidAdjacentTiles(target))
                {
                    if (minDistance < this.findManhattanDistance(candidate))
                        continue;
                    int distance;
                    if ((distance = findPath(candidate)) < minDistance)
                        minDistance = distance;
                }
            }
        }

        int findPath(Coordinate candidate)
        {
            return 0;
        }
        
        int calculateF(NodeAStar candidate, Coordinate goal)
        {
            return candidate.distFromStart + candidate.findManhattanDistance(goal);
        }

        List<Coordinate> getValidAdjacentTiles(Coordinate center)
        {
            List<Coordinate> validTiles = new List<Coordinate>();
            foreach (Coordinate direction in directions)
            {
                Coordinate candidate = center.AddVector(direction);
                if (grid[candidate.y][candidate.x] == '.')
                    validTiles.Add(candidate);
            }
            return validTiles;
        }

        void addValidAdjacentTiles(NodeAStar start)
        {
            foreach (Coordinate direction in directions)
            {
                Coordinate space = start.AddVector(direction);
                if (grid[space.y][space.x] == '.')
                {
                    NodeAStar candidate = new NodeAStar(space.x, space.y, start.distFromStart + 1, start);
                    if (closedNodes.Contains(candidate))
                        continue;
                    else if(openNodes.Contains(candidate))
                    {
                        if (openNodes.Find(x => x == candidate).distFromStart > candidate.distFromStart)
                            openNodes[openNodes.IndexOf(candidate)].distFromStart = candidate.distFromStart;
                    }
                }
            }
        }

        void move()
        {
            Coordinate next = currPath.Pop();
            if (grid[next.y][next.x] != '.')
            {
                Console.WriteLine("Tried to move to invalid tile");
                return;
            }
            this.x = next.x;
            this.y = next.y;
        }
    }

    class Day15AStar
    {
        public static void Run(List<string> input)
        {
        }
    }
}*/
