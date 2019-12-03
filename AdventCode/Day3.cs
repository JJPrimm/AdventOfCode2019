using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCode
{
    public static class Day3
    {
        private class Point : IEquatable<Point>
        {
            public int x { get; set; }
            public int y { get; set; }
            public int step { get; set; }

            public bool Equals(Point other)
            {
                return other.x == x && other.y == y;
            }
            public override bool Equals(object obj) => Equals(obj as Point);
            public override int GetHashCode() => (x, y).GetHashCode();
        }

        public static void Problem1()
        {
            var input = Utilities.ReadStringArray(3);
            var wire1 = GetPath(input[0].Split(','));
            var wire2 = GetPath(input[1].Split(','));

            var intersections = wire1.Intersect(wire2);

            var minDistance = intersections.Select(i => Math.Abs(i.x) + Math.Abs(i.y)).Where(d => d != 0).Min();
            Console.WriteLine($"Day3 - 1: {minDistance}");
        }

        public static void Problem2()
        {
            var input = Utilities.ReadStringArray(3);
            var wire1 = GetPath(input[0].Split(','));
            var wire2 = GetPath(input[1].Split(','));

            var int1 = wire1.Intersect(wire2).ToArray();
            int1 = int1.Where(p => p.step == int1.Where(p1 => p1.Equals(p)).Select(p2 => p2.step).Min()).ToArray();
            var int2 = wire2.Intersect(wire1).ToArray();
            int2 = int2.Where(p => p.step == int2.Where(p1 => p1.Equals(p)).Select(p2 => p2.step).Min()).ToArray();

            var minSteps =  int1.Select(p => p.step + int2.Where(p1 => p.Equals(p1)).First().step).Min();

            Console.WriteLine($"Day3 - 2: {minSteps}");
        }

        private static IEnumerable<Point> GetPath(string[] vectors)
        {
            var path = new List<Point>();
            int x = 0;
            int y = 0;
            int step = 0;
            foreach(var vector in vectors)
            {
                char direction = vector[0];
                int amplitude = Convert.ToInt32(vector.Substring(1));
                for(int i = 0; i < amplitude; i++)
                {
                    switch (direction)
                    {
                        case 'U':
                            y++;
                            break;
                        case 'D':
                            y--;
                            break;
                        case 'L':
                            x++;
                            break;
                        case 'R':
                            x--;
                            break;
                    }
                    step++;
                    var point = new Point() { x = x, y = y, step = step };
                    path.Add(point);
                }
            }
            return path;
        }
    }
}
