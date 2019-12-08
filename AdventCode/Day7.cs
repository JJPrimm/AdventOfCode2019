using System;
using System.Linq;

namespace AdventCode
{
    public static class Day7
    {
        public class Amplifier
        {
            public Amplifier()
            {
                Program = Utilities.ReadCommaSeparatedIntArray(7);
            }
            public int[] Program { get; set; }
            public int Phase { get; set; }
            public int InstructionPtr { get; set; } = 0;
            public int Input { get; set; }
            public int Output { get; set; }
            public bool PhaseInput { get; set; }
        }

        public static Amplifier[] amps = new Amplifier[5];

        public static void Problem1()
        {
            int maxOutput = 0;

            for (int a = 0; a < 5; a++)
            {
                for (int b = 0; b < 5; b++)
                {
                    if (b != a)
                    {
                        for (int c = 0; c < 5; c++)
                        {
                            if (c != a && c != b)
                            {
                                for (int d = 0; d < 5; d++)
                                {
                                    if (d != a && d != b && d != c)
                                    {
                                        for (int e = 0; e < 5; e++)
                                        {
                                            if (e != a && e != b && e != c && e != d)
                                            {
                                                int[] phases = { a, b, c , d, e };
                                                int output = 0;

                                                for (int i = 0; i < 5; i++)
                                                {
                                                    amps[i] = new Amplifier();
                                                    amps[i].Phase = phases[i];
                                                    amps[i].Input = output;
                                                    RunProgram(i);
                                                    output = amps[i].Output;
                                                }
                                                Console.WriteLine($"[{a},{b},{c},{d},{e}] = {output}");
                                                maxOutput = (output > maxOutput) ? output : maxOutput;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Day 7 - 1: {maxOutput}");
        }

        public static void Problem2()
        {
            int maxOutput = 0;

            for (int a = 0; a < 5; a++)
            {
                for (int b = 0; b < 5; b++)
                {
                    if (b != a)
                    {
                        for (int c = 0; c < 5; c++)
                        {
                            if (c != a && c != b)
                            {
                                for (int d = 0; d < 5; d++)
                                {
                                    if (d != a && d != b && d != c)
                                    {
                                        for (int e = 0; e < 5; e++)
                                        {
                                            if (e != a && e != b && e != c && e != d)
                                            {
                                                int[] phases = { a, b, c, d, e };
                                                phases = phases.Select(p => p + 5).ToArray();

                                                amps = amps.Select(amp => new Amplifier()).ToArray();
                                                for (int i = 0; i < 5; i++)
                                                {
                                                    amps[i].Phase = phases[i];
                                                }
                                                int ampPtr = 0;
                                                while (RunProgram(ampPtr))
                                                {
                                                    var nextPtr = (ampPtr + 1) % 5;
                                                    amps[nextPtr].Input = amps[ampPtr].Output;
                                                    ampPtr = nextPtr;
                                                }
                                                maxOutput = (amps[4].Output > maxOutput) ? amps[4].Output : maxOutput;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Day 7 - 1: {maxOutput}");
        }

        private static bool RunProgram(int ampPtr)
        {
            var memory = amps[ampPtr].Program;
            var i = amps[ampPtr].InstructionPtr;
            int inputPtr = 0;
            int opCode;
            int param1Ptr;
            int param2Ptr;
            int param3Ptr;
            int instructionLength = 0;

            while ((memory[i] % 100) != 99)
            {
                opCode = memory[i] % 100;
                param1Ptr = ((memory[i] % 1000) / 100 == 0) ? memory[i + 1] : i + 1;
                param2Ptr = ((memory[i] % 10000) / 1000 == 0) ? memory[i + 2] : i + 2;
                instructionLength = 0;

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
                        inputPtr++;
                        break;
                    case 4:
                        amps[ampPtr].Output = memory[param1Ptr];
                        amps[ampPtr].InstructionPtr = i + 2;
                        return true;
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
                        param3Ptr = ((memory[i] % 100000) / 10000 == 0) ? memory[i + 3] : i + 3;
                        memory[param3Ptr] = (memory[param1Ptr] < memory[param2Ptr]) ? 1 : 0;
                        instructionLength = 4;
                        break;
                    case 8:
                        param3Ptr = ((memory[i] % 100000) / 10000 == 0) ? memory[i + 3] : i + 3;
                        memory[param3Ptr] = (memory[param1Ptr] == memory[param2Ptr]) ? 1 : 0;
                        instructionLength = 4;
                        break;
                }
                i += instructionLength;
            }
            return false;
        }
    }
}
