using System;

namespace AdventCode
{
    public static class Day4
    {
        public static void Problem1()
        {
            int min = 130254;
            int max = 678275;
            int i = min;
            int results = 0;
            while (i <= max)
            {
                bool repeats = false;
                bool ascending = true;
                int[] ary = Utilities.IntToDigitArray(i);
                for (int d = 0; d < 5; d++)
                {
                    if (ary[d] == ary[d+1])
                    {
                        repeats = true;
                    }
                    if (ary[d] > ary[d+1])
                    {
                        ascending = false;
                        break;
                    }
                }
                if (repeats && ascending)
                {
                    results++;
                }
                i++;
            }

            Console.WriteLine($"Day4 - 1: {results}");
        }

        public static void Problem2()
        {
            int min = 130254;
            int max = 678275;
            int i = min;
            int results = 0;
            while (i <= max)
            {
                bool repeating = false;
                bool repealed = false;
                int repeats = 0;
                bool ascending = true;
                int[] ary = Utilities.IntToDigitArray(i);
                for (int d = 0; d < 5; d++)
                {
                    if (ary[d] == ary[d + 1])
                    {
                        if (repeating)
                        {
                            if (!repealed)
                            {
                                repeats--;
                                repealed = true;
                            }
                        }
                        else
                        {
                            repeats++;
                        }
                        repeating = true;
                    }
                    else
                    {
                        repeating = false;
                        repealed = false;
                    }
                    if (ary[d] > ary[d + 1])
                    {
                        ascending = false;
                        break;
                    }
                }
                if (repeats > 0 && ascending)
                {
                    results++;
                }
                i++;
            }

            Console.WriteLine($"Day4 - 2: {results}");
        }
    }
}
