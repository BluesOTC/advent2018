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
            foreach(string match in backtracks)
                regexMap.Replace(match, "");
            Console.WriteLine(traverse(0));
        }

        static int traverse(int index)
        {
            int distance = 0;
            for (; index < regexMap.Length; index++)
            {
                if (regexMap[index] == ')' || regexMap[index] == '$')
                    return distance;
                if (regexMap[index] == '(')
                {
                    for (int branchIndex = index; true; branchIndex++)
                    {
                        if (regexMap[branchIndex] == '|')
                            return distance + traverseJunction(index + 1); //won't work for nested parens, split into separate function
                    }
                }
                distance++;
            }
            return -1; //this should never happen
        }

        static int traverseJunction(int index)
        {
            return 0; //either use ref index to jump to the right places or push/pop start parens onto a stack to find the right | to branch with
        }
    }
}
