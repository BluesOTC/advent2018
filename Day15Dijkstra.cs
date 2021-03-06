﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.IO;

namespace Advent
{
    class Node : Coordinate, IComparable<Node>
    {
        public Node parent;

        public Node(int x, int y, Node parent) : base(x, y)
        {
            this.parent = parent;
        }

        public int CompareTo(Node other)
        {
            int diff;
            if ((diff = this.y - other.y) != 0)
                return diff;
            return this.x - other.x;
        }
    }

    class Entity : Coordinate, IComparable<Entity>
    {
        List<Coordinate> directions = new List<Coordinate> { new Coordinate(0, -1), new Coordinate(-1, 0), new Coordinate(1, 0), new Coordinate(0, 1) }; //sorted for reading order

        public int hp;
        public int damage;
        List<char[]> grid;
        public char entityType;

        public Entity(int x, int y, char type, int damage, ref List<char[]> grid) : base(x, y)
        {
            entityType = type;
            hp = 200;
            this.damage = damage;
            this.grid = grid;
        }

        public void findBestPath(List<Entity> targets)
        {
            HashSet<Node> candidateGoals = getCandidateGoals(targets);
            if (candidateGoals.Count == 0)
                return;

            HashSet<Node> openNodes = new HashSet<Node>();
            HashSet<Node> closedNodes = new HashSet<Node>();
            bool targetFound = false;
            addValidAdjacentTiles(new Node(this.x, this.y, null), ref openNodes, ref closedNodes);
            Coordinate next = null;
            while (!targetFound)
            {
                if (openNodes.Count == 0)
                {
                    return;
                }
                else
                {
                    List<Node> tilesInRange = new List<Node>();
                    foreach (Node candidate in openNodes)
                    {
                        if (candidateGoals.Contains(candidate))
                        {
                            targetFound = true;
                            tilesInRange.Add(candidate);
                        }
                    }

                    if (targetFound)
                    {
                        tilesInRange.Sort();
                        next = tracePath(tilesInRange[0]);
                        break;
                    }

                    HashSet<Node> newNodes = new HashSet<Node>();
                    foreach (Node candidate in openNodes)
                        addValidAdjacentTiles(candidate, ref newNodes, ref closedNodes);
                    openNodes = newNodes;
                }
            }
            grid[y][x] = '.';
            this.x += next.x;
            this.y += next.y;
            grid[y][x] = entityType;
        }

        void addValidAdjacentTiles(Node start, ref HashSet<Node> openNodes, ref HashSet<Node> closedNodes)
        {
            closedNodes.Add(start);
            foreach (Coordinate direction in directions)
            {
                Coordinate space = start.AddVector(direction);
                if (grid[space.y][space.x] == '.')
                {
                    Node candidate = new Node(space.x, space.y, start);
                    if (!closedNodes.Contains(candidate))
                    {
                        openNodes.Add(candidate);
                        closedNodes.Add(candidate);
                    }
                }
            }
        }

        HashSet<Node> getCandidateGoals(List<Entity> targets)
        {
            HashSet<Node> candidates = new HashSet<Node>();
            foreach (Entity enemy in targets)
            {
                foreach (Coordinate direction in directions)
                {
                    Coordinate candidate = enemy.AddVector(direction);
                    if (grid[candidate.y][candidate.x] == '.')
                        candidates.Add(new Node(candidate.x, candidate.y, null));
                }
            }
            return candidates;
        }

        Coordinate tracePath(Node destination)
        {
            Node next = destination;
            Coordinate vector = null;
            while (next.parent != null)
            {
                vector = next.GetVector(next.parent);
                next = next.parent;
            }
            return vector;
        }

        public Entity doTurn(List<Entity> targets)
        {
            if (this.hp <= 0)
                return null;
            List<Entity> targetsInRange = new List<Entity>();
            if ((targetsInRange = findAttackTargets(targets)).Count > 0)
                return doAttack(targetsInRange);
            else
            {
                findBestPath(targets);
                if ((targetsInRange = findAttackTargets(targets)).Count > 0)
                    return doAttack(targetsInRange);
            }
            return null;
        }

        List<Entity> findAttackTargets(List<Entity> targets)
        {
            List<Entity> targetsInRange = new List<Entity>();
            foreach (Entity enemy in targets)
            {
                if (findManhattanDistance(enemy) == 1)
                    targetsInRange.Add(enemy);
            }
            return targetsInRange;
        }

        Entity doAttack(List<Entity> targets)
        {
            targets.Sort();
            int minHP = 201;
            Entity bestTarget = null;
            foreach (Entity enemy in targets)
            {
                if (enemy.hp < minHP)
                {
                    minHP = enemy.hp;
                    bestTarget = enemy;
                }
            }
            bestTarget.hp -= this.damage;
            if (bestTarget.hp <= 0)
            {
                grid[bestTarget.y][bestTarget.x] = '.';
                return bestTarget;
            }
            return null;
        }

        int IComparable<Entity>.CompareTo(Entity other)
        {
            int diff;
            if ((diff = this.y - other.y) != 0)
                return diff;
            return this.x - other.x;
        }
    }

    class Day15Dijkstra
    {
        public static void Run(bool debug, int sleepTime)
        {
            if (debug)
                Console.Clear();
            else
                Console.WriteLine("\nDay 15");

            List<string> input = new List<string>();
            using (StreamReader reader = new StreamReader("input/input15.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    input.Add(line);
            }

            List<char[]> grid = new List<char[]>();
            List<Entity> entities = new List<Entity>();

            bool combatOver = false;
            bool elfDead = true;
            int elfDamage = 3;
            int round = 0;
            int baseRound = 0;
            int baseHPSum = 0;
            while (elfDead)
            {
                entities.Clear();
                grid.Clear();
                for (int y = 0; y < input.Count; y++)
                {
                    grid.Add(input[y].ToCharArray());
                    for (int x = 0; x < input[y].Length; x++)
                    {
                        if (grid[y][x] == 'G')
                            entities.Add(new Entity(x, y, 'G', 3, ref grid));
                        else if (grid[y][x] == 'E')
                            entities.Add(new Entity(x, y, 'E', elfDamage, ref grid));
                    }
                }
                elfDead = false;
                round = 0;
                while (!combatOver)
                {
                    entities.Sort();
                    List<Entity> entityCopy = new List<Entity>(entities);
                    foreach (Entity e in entityCopy)
                    {
                        if (!entities.Contains(e))
                            continue;
                        List<Entity> filteredEntities = entities.Where(x => x.entityType != e.entityType).ToList();
                        if (filteredEntities.Count == 0)
                        {
                            combatOver = true;
                            break;
                        }
                        Entity killed;
                        if ((killed = e.doTurn(filteredEntities)) != null)
                        {
                            if (killed.entityType == 'E')
                            {
                                //Console.WriteLine("An elf died :(");
                                elfDead = true;
                                if (elfDamage != 3)
                                {
                                    combatOver = true;
                                    break;
                                }
                            }
                            entities.Remove(killed);
                        }
                    }
                    if (combatOver)
                        break;

                    round++;
                    if (debug)
                    {
                        Console.WriteLine("R" + round);
                        for (int y = 0; y < grid.Count; y++)
                        {
                            Console.Write(new string(grid[y]) + " ");
                            foreach (Entity e in entities)
                            {
                                if (e.y == y)
                                    Console.Write(String.Format("{0}({1}), ", e.entityType, e.hp));
                            }
                            Console.WriteLine();
                        }
                        Thread.Sleep(sleepTime); //for watching animated map movement
                        Console.Clear();
                    }
                }
                if (elfDamage == 3)
                {
                    baseRound = round;
                    baseHPSum = entities.Where(x => x.hp > 0).Sum(x => x.hp);
                }
                elfDamage++;
                //if (!elfDead)
                //    Console.WriteLine("All elves survive when they deal " + elfDamage + " damage");
                combatOver = false;
            }
            Console.WriteLine(String.Format("Base Case: Rounds: {0}, Total HP Remaining: {1}, Outcome: {2}", baseRound, baseHPSum, baseRound * baseHPSum));
            int hpSum;
            Console.WriteLine(String.Format("Elf Survival Case: Rounds: {0}, Total HP Remaining: {1}, Outcome: {2}", round, hpSum = entities.Where(x => x.hp > 0).Sum(x => x.hp), round * hpSum));
        }
    }
}
