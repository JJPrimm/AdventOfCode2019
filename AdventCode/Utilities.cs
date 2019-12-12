using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventCode
{
    public static class Utilities
    {
        //public static string ReadString(int day)
        //{
        //    return ReadString
        //}

        public static int[] ReadIntArray(int day)
        {
            return File.ReadAllLines($@"..\..\input\Input{day}.txt")
                .Select(s => Convert.ToInt32(s))
                .ToArray();
        }

        public static string[] ReadStringArray(int day)
        {
            return ReadStringArray($"Input{day}.txt");
        }

        public static string[] ReadStringArray(string file)
        {
            return File.ReadAllLines($@"..\..\input\{file}")
                .ToArray();
        }

        public static int[] ReadSpaceSeparatedIntArray(int day)
        {
            return File.ReadAllText($@"..\..\input\Input{day}.txt").Split(' ').Select(s => Convert.ToInt32(s)).ToArray();
            //return File.ReadAllText($@"..\..\input\Test8.txt").Split(' ').Select(s => Convert.ToInt32(s)).ToArray();
        }

        public static int[] ReadCommaSeparatedIntArray(int day)
        {
            return File.ReadAllText($@"..\..\input\Input{day}.txt").Split(',').Select(s => Convert.ToInt32(s)).ToArray();
            //return File.ReadAllText($@"..\..\input\Test8.txt").Split(' ').Select(s => Convert.ToInt32(s)).ToArray();
        }

        public static long[] ReadCommaSeparatedLongArray(int day)
        {
            return File.ReadAllText($@"..\..\input\Input{day}.txt").Split(',').Select(s => Convert.ToInt64(s)).ToArray();
            //return File.ReadAllText($@"..\..\input\Test8.txt").Split(' ').Select(s => Convert.ToInt32(s)).ToArray();
        }

        public static Dictionary<char, int> CharToInt()
        {
            return new Dictionary<char, int>
            {
                { 'A', 1 },
                { 'B', 2 },
                { 'C', 3 },
                { 'D', 4 },
                { 'E', 5 },
                { 'F', 6 },
                { 'G', 7 },
                { 'H', 8 },
                { 'I', 9 },
                { 'J', 10 },
                { 'K', 11 },
                { 'L', 12 },
                { 'M', 13 },
                { 'N', 14 },
                { 'O', 15 },
                { 'P', 16 },
                { 'Q', 17 },
                { 'R', 18 },
                { 'S', 19 },
                { 'T', 20 },
                { 'U', 21 },
                { 'V', 22 },
                { 'W', 23 },
                { 'X', 24 },
                { 'Y', 25 },
                { 'Z', 26 }
            };
        }

        public static int[] IntToDigitArray(int i, int length = 0)
        {
            var aryLength = (length == 0) ? (int)Math.Floor(Math.Log10(i)) + 1 : length;
            var ary = new int[aryLength];

            for (int x = 1; x <= aryLength; x++)
            {
                ary[aryLength - x] = i % 10;
                i /= 10;
            }
            return ary;
        }

        public static void HeapPermutation(int[] a, int size, int n)
        {
            if (size == 1)
            {
                WriteContainer(a);
            }

            for (int i = 0; i < size; i++)
            {
                HeapPermutation(a, size - 1, n);
                if (size % 2 == 1)
                {
                    int temp = a[0];
                    a[0] = a[size - 1];
                    a[size - 1] = temp;
                }
                else
                {
                    int temp = a[i];
                    a[i] = a[size - 1];
                    a[size - 1] = temp;
                }
            }
        }

        public static void WriteContainer(IEnumerable<int> c)
        {
            StringBuilder str = new StringBuilder("{");
            foreach (int i in c)
            {
                str.Append($" {i}");
            }
            str.Append(" }");
            Console.WriteLine(str.ToString());
        }

        public static double GetAngle(double a, double o)
        {
            double angle;
            if (a == 0)
            {
                if (o < 0)
                {
                    angle = 1.5 * Math.PI;
                }
                else
                {
                    angle = .5 * Math.PI;
                }
            }
            else
            {
                angle = Math.Atan(o / a);
                if (a < 0)
                {
                    angle += Math.PI;
                }
                else if (o < 0)
                {
                    angle += 2 * Math.PI;
                }
            }
            return angle;
        }
    }
}
