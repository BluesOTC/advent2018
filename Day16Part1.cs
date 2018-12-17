using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Advent
{
    public enum OperationType
    {
        ADDR,
        ADDI,
        MULR,
        MULI,
        BANR,
        BANI,
        BORR,
        BORI,
        SETR,
        SETI,
        GTIR,
        GTRI,
        GTRR,
        EQIR,
        EQRI,
        EQRR,
        NUM_TYPES
    }

    class Day16Part1
    {
        public static void Run()
        {
            Console.WriteLine();
            Console.WriteLine("Day 16");

            List<string> input = new List<string>();
            using (StreamReader reader = new StreamReader("input16-1.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    input.Add(line);
            }

            int total = 0;
            List<List<OperationType>> opcodeCandidates = new List<List<OperationType>>();
            for(OperationType op = OperationType.ADDR; op < OperationType.NUM_TYPES; op++)
                opcodeCandidates.Add(Enumerable.Range(0, (int)OperationType.NUM_TYPES).Cast<OperationType>().ToList());
            
            for (int index = 0; index < input.Count - 2; index = index + 4)
            {
                int[] before = new int[] { input[index][9] - '0', input[index][12] - '0', input[index][15] - '0', input[index][18] - '0' };
                string[] operation = input[index + 1].Split(' ');
                int[] operands = new int[] { Int32.Parse(operation[1]), Int32.Parse(operation[2]), Int32.Parse(operation[3]) };
                int currOpcode = Int32.Parse(operation[0]);
                int[] after = new int[] { input[index + 2][9] - '0', input[index + 2][12] - '0', input[index + 2][15] - '0', input[index + 2][18] - '0' };
                int validOpcodes = 0;
                for(OperationType opcode = OperationType.ADDR; opcode < OperationType.NUM_TYPES; opcode++)
                {
                    int[] result = doOperation(opcode, before, operands);
                    bool validOp = true;
                    for(int i = 0; i < result.Length; i++)
                    {
                        if (result[i] != after[i])
                        {
                            opcodeCandidates[currOpcode].Remove(opcode);
                            validOp = false;
                            break;
                        }
                    }
                    if (validOp)
                    {
                        validOpcodes++;
                        if (validOpcodes == 3)
                            total++;
                    }
                }
            }
            Dictionary<int, OperationType> assignedOpcodes = new Dictionary<int, OperationType>();
            while(assignedOpcodes.Count < (int)OperationType.NUM_TYPES)
            {
                for (int index = 0; index < (int)OperationType.NUM_TYPES; index++)
                {
                    if (assignedOpcodes.ContainsKey(index))
                        continue;
                    List<OperationType> toRemove = assignedOpcodes.Values.ToList();
                    opcodeCandidates[index].RemoveAll(x => toRemove.Contains(x));
                }
                for (int index = 0;index < (int)OperationType.NUM_TYPES; index++)
                {
                    if (assignedOpcodes.ContainsKey(index))
                        continue;
                    if (opcodeCandidates[index].Count == 1)
                        assignedOpcodes.Add(index, opcodeCandidates[index][0]);
                }
            }
            /*for (int index = 0; index < opcodeCandidates.Count; index++)
            {
                foreach (OperationType op in opcodeCandidates[index])
                    Console.WriteLine(index + ": " + op);
            }*/
            Console.WriteLine(String.Format("{0} samples can be generated from 3+ opcodes", total));
            
            input = new List<string>();
            using (StreamReader reader = new StreamReader("input16-2.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    input.Add(line);
            }

            int[] registers = new int[] { 0, 0, 0, 0 };
            for (int index = 0; index < input.Count; index++)
            {
                string[] operation = input[index].Split(' ');
                int[] operands = new int[] { Int32.Parse(operation[1]), Int32.Parse(operation[2]), Int32.Parse(operation[3]) };
                OperationType currOpcode = assignedOpcodes[Int32.Parse(operation[0])];
                registers = doOperation(currOpcode, registers, operands);
            }
            Console.WriteLine("Test program output: " + registers[0]);
        }

        static int[] doOperation(OperationType opcode, int[] before, int[] operands)
        {
            int[] result = new int[4];
            before.CopyTo(result, 0);
            switch(opcode)
            {
                case OperationType.ADDI:
                    result[operands[2]] = before[operands[0]] + operands[1];
                    return result;
                case OperationType.ADDR:
                    result[operands[2]] = before[operands[0]] + before[operands[1]];
                    return result;
                case OperationType.MULI:
                    result[operands[2]] = before[operands[0]] * operands[1];
                    return result;
                case OperationType.MULR:
                    result[operands[2]] = before[operands[0]] * before[operands[1]];
                    return result;
                case OperationType.BANI:
                    result[operands[2]] = before[operands[0]] & operands[1];
                    return result;
                case OperationType.BANR:
                    result[operands[2]] = before[operands[0]] & before[operands[1]];
                    return result;
                case OperationType.BORI:
                    result[operands[2]] = before[operands[0]] | operands[1];
                    return result;
                case OperationType.BORR:
                    result[operands[2]] = before[operands[0]] | before[operands[1]];
                    return result;
                case OperationType.SETI:
                    result[operands[2]] = operands[0];
                    return result;
                case OperationType.SETR:
                    result[operands[2]] = before[operands[0]];
                    return result;
                case OperationType.EQIR:
                    result[operands[2]] = operands[0] == before[operands[1]] ? 1 : 0;
                    return result;
                case OperationType.EQRI:
                    result[operands[2]] = before[operands[0]] == operands[1] ? 1 : 0;
                    return result;
                case OperationType.EQRR:
                    result[operands[2]] = before[operands[0]] == before[operands[1]] ? 1 : 0;
                    return result;
                case OperationType.GTIR:
                    result[operands[2]] = operands[0] > before[operands[1]] ? 1 : 0;
                    return result;
                case OperationType.GTRI:
                    result[operands[2]] = before[operands[0]] > operands[1] ? 1 : 0;
                    return result;
                case OperationType.GTRR:
                    result[operands[2]] = before[operands[0]] > before[operands[1]] ? 1 : 0;
                    return result;
                default:
                    return null;
            }
        }
    }
}
