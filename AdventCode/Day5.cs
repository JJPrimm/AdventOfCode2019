using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode
{
    public static class Day5
    {
        public static void Problem1()
        {

            RunProgram(1);
        }

        public static void Problem2()
        {
            RunProgram(5);
        }

        private static void RunProgram(int input)
        {
            var memory = Utilities.ReadCommaSeparatedIntArray(5);
            var i = 0;
            while ((memory[i] % 100) != 99)
            {
                var config = Utilities.IntToDigitArray(memory[i], 5);
                var opCode = 10 * config[3] + config[4];
                var param1Ptr = (config[2] == 0) ? memory[i + 1] : i + 1;
                var param2Ptr = (config[1] == 0) ? memory[i + 2] : i + 2;
                var param3Ptr = (config[0] == 0) ? memory[i + 3] : i + 3;
                int instructionLength = 0;

                switch (opCode)
                {
                    case 1:
                        memory[memory[i + 3]] = memory[param1Ptr] + memory[param2Ptr];
                        instructionLength = 4;
                        break;
                    case 2:
                        memory[memory[i + 3]] = memory[param1Ptr] * memory[param2Ptr];
                        instructionLength = 4;
                        break;
                    case 3:
                        memory[param1Ptr] = input;
                        instructionLength = 2;
                        break;
                    case 4:
                        Console.WriteLine(memory[param1Ptr]);
                        instructionLength = 2;
                        break;
                    case 5:
                        if (memory[param1Ptr] != 0)
                        {
                            i = memory[param2Ptr];
                            instructionLength = 0;
                        }
                        else
                        {
                            instructionLength = 3;
                        }
                        break;
                    case 6:
                        if (memory[param1Ptr] == 0)
                        {
                            i = memory[param2Ptr];
                            instructionLength = 0;
                        }
                        else
                        {
                            instructionLength = 3;
                        }
                        break;
                    case 7:
                        memory[param3Ptr] = (memory[param1Ptr] < memory[param2Ptr]) ? 1 : 0;
                        instructionLength = 4;
                        break;
                    case 8:
                        memory[param3Ptr] = (memory[param1Ptr] == memory[param2Ptr]) ? 1 : 0;
                        instructionLength = 4;
                        break;
                }
                i += instructionLength;
            }
        }

    }
}
