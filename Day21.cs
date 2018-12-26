using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Advent
{
    class Day21
    {
        public static void Run()
        {
            Console.WriteLine("\nDay 21");

            List<Instruction> input = new List<Instruction>();
            int boundRegister;
            using (StreamReader reader = new StreamReader("input/input21.txt"))
            {
                string line = reader.ReadLine();
                boundRegister = line[4] - '0';
                while ((line = reader.ReadLine()) != null)
                {
                    string[] splitLine = line.Split(' ');
                    input.Add(new Instruction(int.Parse(splitLine[1]), int.Parse(splitLine[2]), int.Parse(splitLine[3]), Instruction.opDictionary[splitLine[0]]));
                }
            }
            int[] registers = new int[6];
            List<int> values = new List<int>();
            bool part1Printed = false;
            while (registers[boundRegister] >= 0 && registers[boundRegister] < input.Count)
            {
                if (input[registers[boundRegister]].operation == OperationType.EQRR) //capture all possible stopping values until a repeat is found
                {
                    int currValue;
                    if (!values.Contains(currValue = registers[input[registers[boundRegister]].a]))
                        values.Add(currValue);
                    else
                    {
                        Console.WriteLine("Part 2 Value: " + values.Last());
                        break;
                    }
                }
                if (registers[boundRegister] == 18) //jump to end of loop instead of iterating through
                {
                    registers[3] = Math.Max(registers[3], registers[1] / 256);
                    registers[5] = (registers[3] + 1) * 256;
                    registers[2] = 20;
                }
                if (!part1Printed && registers[boundRegister] == 28) //capture the stopping value once all the initialization is done
                {
                    part1Printed = true;
                    Console.WriteLine("Part 1 Value: " + registers[4]);
                }
                input[registers[boundRegister]].doOperation(ref registers);
                registers[boundRegister]++;
            }
        }
    }
}
