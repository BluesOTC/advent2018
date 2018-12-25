using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Advent
{
    public enum AttackType
    {
        SLASHING,
        BLUDGEONING,
        FIRE,
        COLD,
        RADIATTION,
        NUM_TYPES
    }

    class Army
    {
        public long units;
        public long hp;
        public long damage;
        public long initiative;
        public int[] modifiers;
        public AttackType attack;
        public bool isImmuneArmy;

        public Army(long units, long hp, long damage, long initiative, int[] modifiers, AttackType attack, bool bImmune)
        {
            this.units = units;
            this.hp = hp;
            this.damage = damage;
            this.initiative = initiative;
            this.modifiers = modifiers;
            this.attack = attack;
            this.isImmuneArmy = bImmune;
        }

        public long getEffectivePower()
        {
            return units * damage;
        }

        public Army chooseTarget(IEnumerable<Army> candidates)
        {
            Army bestTarget = null;
            long maxDamage = 0;
            foreach (Army candidate in candidates)
            {
                long damage;
                if ((damage = this.calculateDamage(candidate)) > maxDamage) //because the candidate list should already be sorted, the first instance of damage should be the correct target
                {
                    bestTarget = candidate;
                    maxDamage = damage;
                }
            }
            return bestTarget;
        }

        public long calculateDamage(Army target)
        {
            return this.getEffectivePower() * target.modifiers[(int)this.attack];
        }

        public void takeDamage(long damageTaken)
        {
            units -= damageTaken / hp;
        }
    }

    class Day24
    {
        static Dictionary<string, AttackType> typeDict = new Dictionary<string, AttackType> { { "cold", AttackType.COLD }, { "slashing", AttackType.SLASHING }, { "bludgeoning", AttackType.BLUDGEONING }, { "fire", AttackType.FIRE }, { "radiation", AttackType.RADIATTION } };

        static int CompareArmyTargetingOrder(Army x, Army y)
        {
            if (x.getEffectivePower() == y.getEffectivePower())
                return (int)(y.initiative - x.initiative);
            return (int)(y.getEffectivePower() - x.getEffectivePower());
        }

        public static void Run()
        {
            Console.WriteLine("\nDay 24");

            List<Army> origArmies = new List<Army>();
            using (StreamReader reader = new StreamReader("input/input24.txt"))
            {
                string line;
                line = reader.ReadLine();
                bool bImmune = true;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains(':'))
                    {
                        bImmune = false;
                        continue;
                    }
                    if (line.Length == 0)
                        continue;
                    int[] modifiers = Enumerable.Repeat(1, (int)AttackType.NUM_TYPES).ToArray();
                    int resistStart = line.IndexOf('(');
                    if (resistStart != -1)
                    {
                        int resistEnd = line.IndexOf(')');
                        string resistances = line.Substring(resistStart + 1, resistEnd - resistStart - 1);
                        if (resistances.Contains(";")) 
                        {
                            if (resistances[0] == 'i')
                            {
                                string[] resistanceTypes = resistances.Split(';');
                                string[] immuneTypes = resistanceTypes[0].Split(' ', ',');
                                string[] weakTypes = resistanceTypes[1].Split(' ', ',');
                                for (int index = 2; index < immuneTypes.Length; index += 2)
                                    modifiers[(int)typeDict[immuneTypes[index]]] = 0;
                                for (int index = 3; index < weakTypes.Length; index += 2)
                                    modifiers[(int)typeDict[weakTypes[index]]] = 2;
                            }
                            else
                            {
                                string[] resistanceTypes = resistances.Split(';');
                                string[] immuneTypes = resistanceTypes[1].Split(' ', ',');
                                string[] weakTypes = resistanceTypes[0].Split(' ', ',');
                                for (int index = 3; index < immuneTypes.Length; index += 2)
                                    modifiers[(int)typeDict[immuneTypes[index]]] = 0;
                                for (int index = 2; index < weakTypes.Length; index += 2)
                                    modifiers[(int)typeDict[weakTypes[index]]] = 2;
                            }
                        }
                        else if (resistances[0] == 'i')
                        {
                            string[] immuneTypes = resistances.Split(' ', ',');
                            for (int index = 2; index < immuneTypes.Length; index += 2)
                                modifiers[(int)typeDict[immuneTypes[index]]] = 0;
                        }
                        else
                        {
                            string[] weakTypes = resistances.Split(' ', ',');
                            for (int index = 2; index < weakTypes.Length; index += 2)
                                modifiers[(int)typeDict[weakTypes[index]]] = 2;
                        }
                        string[] attributes = line.Split(' ');
                        int units = int.Parse(attributes[0]);
                        int hp = int.Parse(attributes[4]);

                        string[] secondHalf = line.Substring(resistEnd + 2).Split(' ');
                        int damage = int.Parse(secondHalf[5]);
                        AttackType attack = typeDict[secondHalf[6]];
                        int initiative = int.Parse(secondHalf[10]);
                        origArmies.Add(new Army(units, hp, damage, initiative, modifiers, attack, bImmune));
                    }
                    else
                    {
                        string[] attributes = line.Split(' ');
                        int units = int.Parse(attributes[0]);
                        int hp = int.Parse(attributes[4]);
                        int damage = int.Parse(attributes[12]);
                        AttackType attack = typeDict[attributes[13]];
                        int initiative = int.Parse(attributes[17]);
                        origArmies.Add(new Army(units, hp, damage, initiative, modifiers, attack, bImmune));
                    }
                }
            }

            int boost = 0;
            while (true)
            {
                List<Army> allArmies = new List<Army>();
                foreach (Army army in origArmies)
                    allArmies.Add(new Army(army.units, army.hp, army.isImmuneArmy ? army.damage + boost : army.damage, army.initiative, army.modifiers, army.attack, army.isImmuneArmy));
                while (allArmies.Any(x => x.isImmuneArmy) && allArmies.Any(x => !x.isImmuneArmy))
                {
                    //Console.WriteLine("Immune Armies: " + String.Join(", ", allArmies.Where(x => x.isImmuneArmy).Select(x => x.damage)));
                    long startingUnits = allArmies.Sum(x => x.units);
                    Dictionary<Army, Army> targets = new Dictionary<Army, Army>();
                    allArmies.Sort(CompareArmyTargetingOrder);
                    foreach (Army army in allArmies)
                        targets.Add(army, army.chooseTarget(allArmies.Where(x => !targets.ContainsValue(x) && x.isImmuneArmy != army.isImmuneArmy)));
                    foreach (Army attacker in targets.Keys.OrderByDescending(x => x.initiative))
                    {
                        if (targets[attacker] != null && attacker.units > 0)
                            targets[attacker].takeDamage(attacker.calculateDamage(targets[attacker]));
                    }
                    allArmies.RemoveAll(x => x.units <= 0);
                    if (startingUnits == allArmies.Sum(x => x.units))
                        break;
                }
                if (boost == 0)
                    Console.WriteLine("Part 1 -Surviving Army's Units: " + allArmies.Sum(x => x.units));
                if (allArmies.All(x => x.isImmuneArmy))
                {
                    Console.WriteLine("Part 2 - Surviving Army's Units: " + allArmies.Sum(x => x.units));
                    return;
                }
                boost++;
            }
        }
    }
}
