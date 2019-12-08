using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCode
{
    public static class Day8
    {

        public static void Problem1()
        {
            var input = Utilities.ReadStringArray(8)[0];
            int layerCount = input.Length / 150;
            int leastZeroes = 1000;
            int leastZeroesLayer = 0;
            string layer;

            for (int l = 0; l < layerCount; l++)
            {
                layer = input.Substring(l * 150, 150);
                var zeroes = layer.Where(c => c == '0').Count();
                if (zeroes < leastZeroes)
                {
                    leastZeroes = zeroes;
                    leastZeroesLayer = l;
                }
            }
            layer = input.Substring(leastZeroesLayer * 150, 150);
            var ones = layer.Where(c => c == '1').Count();
            var twos = layer.Where(c => c == '2').Count();
            var result = ones * twos;

            Console.WriteLine($"Day 8 - 1: {result}");
        }

        public static void Problem2()
        {
            var input = Utilities.ReadStringArray(8)[0];
            int layerCount = input.Length / 150;
            var layers = new List<string>();
            for (int l = 0; l < layerCount; l++)
            {
                layers.Add(input.Substring(l * 150, 150));
            }

            for (int p = 0; p < 150; p++)
            {
                if (p % 25 == 0)
                {
                    Console.WriteLine();
                }
                char color = ' ';
                for (int l = 0; l < layerCount; l++)
                {
                    if (layers[l][p] != '2')
                    {
                        if (layers[l][p] == '1')
                        {
                            color = 'O';
                        }
                        else
                        {
                            color = '.';
                        }
                        break;
                    }
                }
                Console.Write(color);
            }

        }
    }
}
