using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCode
{
    public static class Day2
    {
        public static void Problem1()
        {
            Console.WriteLine($"Day2 - 1: {RunProgram(12, 2)}");
        }

        public static void Problem2()
        {
            bool found = false;

            for (int noun = 0; noun <= 99; noun++)
            {
                for (int verb = 0; verb <= 99; verb++)
                {
                    try
                    {
                        if (RunProgram(noun, verb) == 19690720)
                        {
                            found = true;
                            Console.WriteLine($"Day2 - 2: {100 * noun + verb}");
                        }
                    }
                    catch { }
                }
            }

            if (!found)
            {
                Console.WriteLine($"Day2 - 2: Parameters not found");
            }
        }

        private static int RunProgram(int noun, int verb)
        {
            var memory = Utilities.ReadCommaSeparatedIntArray(2);
            memory[1] = noun;
            memory[2] = verb;
            var i = 0;
            while (memory[i] != 99)
            {
                if (memory[i] == 1)
                {
                    memory[memory[i + 3]] = memory[memory[i + 1]] + memory[memory[i + 2]];
                }
                else if (memory[i] == 2)
                {
                    memory[memory[i + 3]] = memory[memory[i + 1]] * memory[memory[i + 2]];
                }
                i += 4;
            }
            return memory[0];
        }
    }
}
