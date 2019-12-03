using System;
using System.Linq;

namespace AdventCode
{
    public static class Day1
    {
        public static void Problem1()
        {
            var input = Utilities.ReadIntArray(1);

            var sum = input.Select(m => (m / 3) - 2).Sum();

            Console.WriteLine($"Day1 - 1: {sum}");
        }

        public static void Problem2()
        {
            var input = Utilities.ReadIntArray(1);

            var sum = input.Select(m => CalcFuelRequired(m)).Sum();

            
            Console.WriteLine($"Day1 - 2: {sum}");
        }

        private static int CalcFuelRequired(int m)
        {
            var fuelRequired = (m / 3) - 2;
            return (fuelRequired > 0) ? fuelRequired + CalcFuelRequired(fuelRequired) : 0;
        }
    }
}
