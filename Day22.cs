using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Advent
{
    class CaveNode : Coordinate, IEquatable<CaveNode>
    {
        public int terrain;
        public int equipment;
        public int costToNode;
        int estimatedTimeLeft;
        public int f;

        public CaveNode(CaveNode prevNode, int x, int y, int targetX, int targetY, int terrain, int equipment) : base(x, y)
        {
            this.x = x;
            this.y = y;
            this.terrain = terrain;
            this.equipment = equipment >= 3 ? equipment % 3 : equipment;
            costToNode = calculateCostToNode(prevNode);
            estimatedTimeLeft = this.findManhattanDistance(targetX, targetY);
            estimatedTimeLeft += this.equipment == 0 ? 0 : 7;
            f = costToNode + estimatedTimeLeft;
        }

        int calculateCostToNode(CaveNode prevNode)
        {
            if (prevNode == null)
                return 0;
            int g = prevNode.costToNode + 1;
            if (prevNode.equipment != this.equipment)
                g += 7;
            return g;
        }

        public override bool Equals(object second)
        {
            CaveNode other = (CaveNode)second;
            return this.x == other.x && this.y == other.y && this.equipment == other.equipment;
        }

        public override int GetHashCode()
        {
            return x * 1000 + y + equipment * 1000000;
        }

        bool IEquatable<CaveNode>.Equals(CaveNode other)
        {
            return this.x == other.x && this.y == other.y && this.equipment == other.equipment;
        }
    }

    class Day22
    {
        static HashSet<CaveNode> openNodes = new HashSet<CaveNode>();
        static HashSet<CaveNode> closedNodes = new HashSet<CaveNode>();

        public static void Run()
        {
            Console.WriteLine("\nDay 22");

            int[][] erosionGrid;
            int[][] terrainGrid;
            int targetX;
            int targetY;
            int depth;
            using (StreamReader reader = new StreamReader("input/input22.txt"))
            {
                depth = Int32.Parse(reader.ReadLine().Split(' ')[1]);
                string[] target = reader.ReadLine().Split(' ', ',');
                targetX = Int32.Parse(target[1]);
                targetY = Int32.Parse(target[2]);
                erosionGrid = new int[(targetY + 1) * 2][];
                terrainGrid = new int[(targetY + 1) * 2][];
                for (int row = 0; row < erosionGrid.Length; row++)
                {
                    erosionGrid[row] = new int[(targetX + 1) * 2];
                    terrainGrid[row] = new int[(targetX + 1) * 2];
                }
            }
            for (int y = 0; y < erosionGrid.Length; y++)
            {
                for (int x = 0; x < erosionGrid[y].Length; x++)
                {
                    if (targetX == x && targetY == y)
                        erosionGrid[y][x] = depth % 20183;
                    else if (x == 0)
                    {
                        if (y == 0)
                            erosionGrid[y][x] = depth % 20183;
                        else
                            erosionGrid[y][x] = (y * 48271 + depth) % 20183;
                    }
                    else if (y == 0)
                        erosionGrid[y][x] = (x * 16807 + depth) % 20183;
                    else
                        erosionGrid[y][x] = (erosionGrid[y - 1][x] * erosionGrid[y][x - 1] + depth) % 20183;
                    terrainGrid[y][x] = erosionGrid[y][x] % 3;
                }
            }
            Console.WriteLine("Total Risk Level: " + (terrainGrid.Take(targetY + 1).Sum(x => x.Take(targetX + 1).Sum())));
            
            openNodes.Add(new CaveNode(null, 0, 0, targetX, targetY, 0, 0)); //start with torch
            while (true)
            {
                CaveNode currNode = openNodes.OrderBy(x => x.f).FirstOrDefault();
                if (currNode.x == targetX && currNode.y == targetY)
                {
                    if (currNode.equipment == 0)
                    {
                        Console.WriteLine("Quickest Time to Target: " + currNode.costToNode);
                        break;
                    }
                    else
                    {
                        currNode.equipment = 0;
                        currNode.f += 7;
                    }
                }
                else
                {
                    closedNodes.Add(currNode);
                    openNodes.Remove(currNode);

                    int terrain;
                    if (currNode.x > 0)
                    {
                        terrain = terrainGrid[currNode.y][currNode.x - 1];
                        tryAddNode(new CaveNode(currNode, currNode.x - 1, currNode.y, targetX, targetY, terrain, currNode.terrain));
                        tryAddNode(new CaveNode(currNode, currNode.x - 1, currNode.y, targetX, targetY, terrain, currNode.terrain + 1));
                    }
                    if (currNode.y > 0)
                    {
                        terrain = terrainGrid[currNode.y - 1][currNode.x];
                        tryAddNode(new CaveNode(currNode, currNode.x, currNode.y - 1, targetX, targetY, terrain, currNode.terrain));
                        tryAddNode(new CaveNode(currNode, currNode.x, currNode.y - 1, targetX, targetY, terrain, currNode.terrain + 1));
                    }
                    if (currNode.y < terrainGrid.Length - 1)
                    {
                        terrain = terrainGrid[currNode.y + 1][currNode.x];
                        tryAddNode(new CaveNode(currNode, currNode.x, currNode.y + 1, targetX, targetY, terrain, currNode.terrain));
                        tryAddNode(new CaveNode(currNode, currNode.x, currNode.y + 1, targetX, targetY, terrain, currNode.terrain + 1));
                    }
                    if (currNode.x < terrainGrid[currNode.y].Length - 1)
                    {
                        terrain = terrainGrid[currNode.y][currNode.x + 1];
                        tryAddNode(new CaveNode(currNode, currNode.x + 1, currNode.y, targetX, targetY, terrain, currNode.terrain));
                        tryAddNode(new CaveNode(currNode, currNode.x + 1, currNode.y, targetX, targetY, terrain, currNode.terrain + 1));
                    }
                }
            }
        }

        static void tryAddNode(CaveNode next)
        {
            if (closedNodes.Contains(next) || next.equipment == (next.terrain + 2) % 3)
                return;
            if (openNodes.Contains(next))
            {
                CaveNode candidate;
                openNodes.TryGetValue(next, out candidate);
                candidate.costToNode = Math.Min(candidate.costToNode, next.costToNode);
                candidate.f = Math.Min(candidate.f, next.f);
            }
            else
                openNodes.Add(next);
        }
    }
}
