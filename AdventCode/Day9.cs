using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode
{
    public static class Day9
    {
        public class Amplifier
        {
            public Amplifier()
            {
                Program = Utilities.ReadCommaSeparatedLongArray(9);

            }
            public long[] Program { get; set; }
            public int Phase { get; set; }
            public int InstructionPtr { get; set; } = 0;
            public int Input { get; set; }
            public long Output { get; set; }
            public bool PhaseInput { get; set; }
        }

        public static Amplifier[] amps = new Amplifier[1];

        public static void Problem1()
        {
            amps[0] = new Amplifier();
            amps[0].Phase = 1;

            RunProgram(0);
        }

        public static void Problem2()
        {
            amps[0] = new Amplifier();
            amps[0].Phase = 2;

            RunProgram(0);
        }

        private static bool RunProgram(int ampPtr)
        {
            var memory = amps[ampPtr].Program;
            var i = amps[ampPtr].InstructionPtr;
            int opCode;
            int param1Ptr;
            int param2Ptr;
            int param3Ptr;
            int relativeBase = 0;
            int instructionLength = 0;

            while ((memory[i] % 100) != 99)
            {
                opCode = Convert.ToInt32(memory[i]) % 100;
                instructionLength = 0;
                switch (opCode)
                {
                    case 1:
                        param1Ptr = GetPtr((Convert.ToInt32(memory[i]) % 1000) / 100, i + 1, relativeBase, ref memory);
                        param2Ptr = GetPtr((Convert.ToInt32(memory[i]) % 10000) / 1000, i + 2, relativeBase, ref memory);
                        param3Ptr = GetPtr((Convert.ToInt32(memory[i]) % 100000) / 10000, i + 3, relativeBase, ref memory);
                        memory[param3Ptr] = memory[param1Ptr] + memory[param2Ptr];
                        instructionLength = 4;
                        break;
                    case 2:
                        param1Ptr = GetPtr((Convert.ToInt32(memory[i]) % 1000) / 100, i + 1, relativeBase, ref memory);
                        param2Ptr = GetPtr((Convert.ToInt32(memory[i]) % 10000) / 1000, i + 2, relativeBase, ref memory);
                        param3Ptr = GetPtr((Convert.ToInt32(memory[i]) % 100000) / 10000, i + 3, relativeBase, ref memory);
                        memory[param3Ptr] = memory[param1Ptr] * memory[param2Ptr];
                        instructionLength = 4;
                        break;
                    case 3:
                        param1Ptr = GetPtr((Convert.ToInt32(memory[i]) % 1000) / 100, i + 1, relativeBase, ref memory);
                        if (amps[ampPtr].PhaseInput)
                        {
                            memory[param1Ptr] = amps[ampPtr].Input;
                        }
                        else
                        {
                            memory[param1Ptr] = amps[ampPtr].Phase;
                            amps[ampPtr].PhaseInput = true;
                        }
                        instructionLength = 2;
                        break;
                    case 4:
                        param1Ptr = GetPtr((Convert.ToInt32(memory[i]) % 1000) / 100, i + 1, relativeBase, ref memory);
                        amps[ampPtr].Output = memory[param1Ptr];
                        amps[ampPtr].InstructionPtr = i + 2;
                        Console.WriteLine(memory[param1Ptr]);
                        instructionLength = 2;
                        //return true;
                        break;
                    case 5:
                        param1Ptr = GetPtr((Convert.ToInt32(memory[i]) % 1000) / 100, i + 1, relativeBase, ref memory);
                        if (memory[param1Ptr] != 0)
                        {
                            param2Ptr = GetPtr((Convert.ToInt32(memory[i]) % 10000) / 1000, i + 2, relativeBase, ref memory);
                            i = Convert.ToInt32(memory[param2Ptr]);
                            instructionLength = 0;
                        }
                        else
                        {
                            instructionLength = 3;
                        }
                        break;
                    case 6:
                        param1Ptr = GetPtr((Convert.ToInt32(memory[i]) % 1000) / 100, i + 1, relativeBase, ref memory);
                        if (memory[param1Ptr] == 0)
                        {
                            param2Ptr = GetPtr((Convert.ToInt32(memory[i]) % 10000) / 1000, i + 2, relativeBase, ref memory);
                            i = Convert.ToInt32(memory[param2Ptr]);
                            instructionLength = 0;
                        }
                        else
                        {
                            instructionLength = 3;
                        }
                        break;
                    case 7:
                        param1Ptr = GetPtr((Convert.ToInt32(memory[i]) % 1000) / 100, i + 1, relativeBase, ref memory);
                        param2Ptr = GetPtr((Convert.ToInt32(memory[i]) % 10000) / 1000, i + 2, relativeBase, ref memory);
                        param3Ptr = GetPtr((Convert.ToInt32(memory[i]) % 100000) / 10000, i + 3, relativeBase, ref memory);
                        memory[param3Ptr] = (memory[param1Ptr] < memory[param2Ptr]) ? 1 : 0;
                        instructionLength = 4;
                        break;
                    case 8:
                        param1Ptr = GetPtr((Convert.ToInt32(memory[i]) % 1000) / 100, i + 1, relativeBase, ref memory);
                        param2Ptr = GetPtr((Convert.ToInt32(memory[i]) % 10000) / 1000, i + 2, relativeBase, ref memory);
                        param3Ptr = GetPtr((Convert.ToInt32(memory[i]) % 100000) / 10000, i + 3, relativeBase, ref memory);
                        memory[param3Ptr] = (memory[param1Ptr] == memory[param2Ptr]) ? 1 : 0;
                        instructionLength = 4;
                        break;
                    case 9:
                        param1Ptr = GetPtr((Convert.ToInt32(memory[i]) % 1000) / 100, i + 1, relativeBase, ref memory);
                        relativeBase += Convert.ToInt32(memory[param1Ptr]);
                        instructionLength = 2;
                        break;
                }
                i += instructionLength;
            }
            return false;
        }

        public static  int GetPtr(int mode, long value, int relativeBase, ref long[] memory)
        {
            int ptr;

            switch (mode)
            {
                case 0:
                    ptr = Convert.ToInt32(memory[value]);
                    break;
                case 1:
                    ptr = Convert.ToInt32(value);
                    break;
                case 2:
                    ptr = Convert.ToInt32(memory[value]) + relativeBase;
                    break;
                default:
                    throw new Exception("This ain't right.");
            }
            CheckArraySize(ref memory, ptr);
            return ptr;
        }

        public static void CheckArraySize(ref long[] memory, int ptr)
        {
            if (ptr >= memory.Length)
            {
                Array.Resize(ref memory, ptr + 1);
            }
        }
    }
}
