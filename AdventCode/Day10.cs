using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCode
{
    public static class Day10
    {

        public static string[] input;
        public class Asteroid
        {
            public Asteroid(int x, int y)
            {
                X = x;
                Y = y;
                AnglesFound = new List<double>();
            }
            public int X { get; set; }
            public int Y { get; set; }
            public double AngleFromStation { get; set; } = 0;
            public int DistanceFromStation { get; set; } = 0;

            public List<double> AnglesFound;
            public int AngleCount { get { return AnglesFound.Count(); } }
            public void GetAngles(bool IsStation = false)
            {
                for (int y = 0; y < input.Length; y++)
                {
                    for (int x = 0; x < input[0].Length; x++)
                    {
                        if (input[y][x] == '#' && (X != x || Y != y))
                        {
                            double angle;
                            double relx = x - X;
                            double rely = Y - y;
                            angle = Utilities.GetAngle(relx, rely);
                            if (!IsStation)
                            {
                                if (!AnglesFound.Contains(angle))
                                {
                                    AnglesFound.Add(angle);
                                }
                            }
                            else
                            {
                                var asteroid = asteroids.Where(a => a.X == x && a.Y == y).First();
                                asteroid.AngleFromStation = (angle > startingAngle) ? angle -= 2 * Math.PI : angle;
                                asteroid.DistanceFromStation = Math.Abs(X - x) + Math.Abs(Y - y);
                            }
                        }
                    }
                }
            }
        }

        public static Asteroid station;
        public static List<Asteroid> asteroids;
        public const double startingAngle = .5 * Math.PI;

        public static void Problem1()
        {
            input = Utilities.ReadStringArray(10);
            asteroids = new List<Asteroid>();

            for(int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[0].Length; x++)
                {
                    if(input[y][x] == '#')
                    {
                        var asteroid = new Asteroid(x, y);
                        asteroid.GetAngles();
                        asteroids.Add(asteroid);
                    }
                }
            }
            station = asteroids.Where(a1 => a1.AngleCount == asteroids.Select(a2 => a2.AnglesFound.Count()).Max()).First();
            Console.WriteLine($"Day 10 - 1: {station.AngleCount} ({station.X}, {station.Y})");
        }

        public static void Problem2()
        {
            Problem1();
            station.GetAngles(true);
            asteroids = asteroids.OrderByDescending(a => a.AngleFromStation)
                                 .ThenBy(a => a.DistanceFromStation)
                                 .ToList();
            int count = 0;
            double prevAngle = 0;

            foreach (Asteroid asteroid in asteroids)
            {
                if (prevAngle != asteroid.AngleFromStation)
                {
                    count++;
                    Console.WriteLine($"{count} - ({asteroid.X},{asteroid.Y}) - Angle: {asteroid.AngleFromStation}; - Distance: {asteroid.DistanceFromStation}");
                    if (count == 200)
                    {
                        int result = asteroid.X * 100 + asteroid.Y;
                        Console.WriteLine($"Day 10 - 2: {result}");
                        break;
                    }
                    prevAngle = asteroid.AngleFromStation;
                }
            }
        }
    }
}
