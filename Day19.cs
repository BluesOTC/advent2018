using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Advent
{
    class Day19
    {
        public static void Run()
        {
            Console.WriteLine("\nDay 19");

            List<Instruction> input = new List<Instruction>();
            int boundRegister;
            using (StreamReader reader = new StreamReader("input/input19.txt"))
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
            try //waiting to catch an IndexOutOfBounds
            {
                for (; ; registers[boundRegister]++)
                    input[registers[boundRegister]].doOperation(ref registers);
            }
            catch
            {
                Console.WriteLine("Part 1: " + registers[0]);
            }

            registers = new int[6];
            registers[0] = 1;
            while (registers[boundRegister] >= 0 && registers[boundRegister] < input.Count)
            {
                if (registers[boundRegister] == input.Count - 1)
                    break;
                input[registers[boundRegister]].doOperation(ref registers);
                registers[boundRegister]++;
            }
            int threshold = (int)Math.Sqrt(registers[1]); //search for factors up to sqrt(n)
            for(registers[5] = 1; registers[5] <= threshold; registers[5]++)
            {
                if (registers[1] % registers[5] == 0)
                    registers[0] += registers[5];
            }
            Console.WriteLine("Part 2: " + (registers[0] + registers[1]));
        }
    }
}
