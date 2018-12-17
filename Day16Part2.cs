using System;
using System.Collections.Generic;
using System.IO;

namespace Advent
{
    class Day16Part2
    {
        public static void Run()
        {
        }

        static int[] doOperation(int opcode, int[] before, int[] operands)
        {
            int[] result = new int[4];
            before.CopyTo(result, 0);
            switch (opcode)
            {
                case 5:
                    result[operands[2]] = before[operands[0]] + operands[1];
                    return result;
                case 0:
                    result[operands[2]] = before[operands[0]] + before[operands[1]];
                    return result;
                case 9:
                    result[operands[2]] = before[operands[0]] * operands[1];
                    return result;
                case 14:
                    result[operands[2]] = before[operands[0]] * before[operands[1]];
                    return result;
                case 15:
                    result[operands[2]] = before[operands[0]] & operands[1];
                    return result;
                case 6:
                    result[operands[2]] = before[operands[0]] & before[operands[1]];
                    return result;
                case 8:
                    result[operands[2]] = before[operands[0]] | operands[1];
                    return result;
                case 13:
                    result[operands[2]] = before[operands[0]] | before[operands[1]];
                    return result;
                case 10:
                    result[operands[2]] = operands[0];
                    return result;
                case 12:
                    result[operands[2]] = before[operands[0]];
                    return result;
                case 2:
                    result[operands[2]] = operands[0] == before[operands[1]] ? 1 : 0;
                    return result;
                case 1:
                    result[operands[2]] = before[operands[0]] == operands[1] ? 1 : 0;
                    return result;
                case 3:
                    result[operands[2]] = before[operands[0]] == before[operands[1]] ? 1 : 0;
                    return result;
                case 4:
                    result[operands[2]] = operands[0] > before[operands[1]] ? 1 : 0;
                    return result;
                case 7:
                    result[operands[2]] = before[operands[0]] > operands[1] ? 1 : 0;
                    return result;
                case 11:
                    result[operands[2]] = before[operands[0]] > before[operands[1]] ? 1 : 0;
                    return result;
                default:
                    return result;
            }
        }
    }
}
