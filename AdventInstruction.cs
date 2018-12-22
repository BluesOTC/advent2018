using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    struct Instruction
    {
        public int a { get; set; }
        public int b { get; set; }
        public int c { get; set; }
        public OperationType operation { get; set; }

        public Instruction(int a, int b, int c, OperationType operation)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.operation = operation;
        }

        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3}", operation, a, b, c);
        }

        public void doOperation(ref int[] registers)
        {
            switch (operation)
            {
                case OperationType.ADDI:
                    registers[c] = registers[a] + b;
                    break;
                case OperationType.ADDR:
                    registers[c] = registers[a] + registers[b];
                    break;
                case OperationType.MULI:
                    registers[c] = registers[a] * b;
                    break;
                case OperationType.MULR:
                    registers[c] = registers[a] * registers[b];
                    break;
                case OperationType.BANI:
                    registers[c] = registers[a] & b;
                    break;
                case OperationType.BANR:
                    registers[c] = registers[a] & registers[b];
                    break;
                case OperationType.BORI:
                    registers[c] = registers[a] | b;
                    break;
                case OperationType.BORR:
                    registers[c] = registers[a] | registers[b];
                    break;
                case OperationType.SETI:
                    registers[c] = a;
                    break;
                case OperationType.SETR:
                    registers[c] = registers[a];
                    break;
                case OperationType.EQIR:
                    registers[c] = a == registers[b] ? 1 : 0;
                    break;
                case OperationType.EQRI:
                    registers[c] = registers[a] == b ? 1 : 0;
                    break;
                case OperationType.EQRR:
                    registers[c] = registers[a] == registers[b] ? 1 : 0;
                    break;
                case OperationType.GTIR:
                    registers[c] = a > registers[b] ? 1 : 0;
                    break;
                case OperationType.GTRI:
                    registers[c] = registers[a] > b ? 1 : 0;
                    break;
                case OperationType.GTRR:
                    registers[c] = registers[a] > registers[b] ? 1 : 0;
                    break;
            }
        }
    }
}
