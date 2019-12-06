using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode
{
    public static class Day6
    {
        public class Orbit : IEquatable<Orbit>
        {
            public string Parent { get; set; }
            public string Child { get; set; }

            public int OrbitCount()
            {
                var parentOrbit = GetParent();

                return 1 + ((parentOrbit == null) ? 0 : parentOrbit.OrbitCount());
            }

            public int GetDistanceFromAncestor(string obj)
            {
                if (obj == Parent)
                {
                    return 0;
                }
                else
                {
                    return 1 + GetParent().GetDistanceFromAncestor(obj);
                }
            }

            public Orbit GetParent()
            {
                return orbits.FirstOrDefault(o => o.Child == Parent);
            }

            public IEnumerable<string> GetAncestors()
            {
                if (Parent == "COM")
                {
                    return new List<string>();
                }
                else
                {
                    var ancestors = GetParent().GetAncestors().ToList();
                    ancestors.Add(Parent);
                    return ancestors;
                }
            }

            public bool Equals(Orbit other)
            {
                return other.Parent == Parent && other.Child == Child;
            }

            public override bool Equals(object obj) => Equals(obj as Orbit);
        }

        public static IEnumerable<Orbit> orbits;

        public static void Problem1()
        {
            var input = Utilities.ReadStringArray(6);
            orbits = input.Select(i => new Orbit() { Child = i.Split(')')[1], Parent = i.Split(')')[0] });
            Console.WriteLine("Day 6 - 1: Orbits Loaded");
            int orbitCount = 0;

            foreach(Orbit orbit in orbits)
            {
                orbitCount += orbit.OrbitCount();
            }

            Console.WriteLine($"Day 6 - 1: {orbitCount}");
        }

        public static void Problem2()
        {
            var input = Utilities.ReadStringArray(6);
            orbits = input.Select(i => new Orbit() { Child = i.Split(')')[1], Parent = i.Split(')')[0] });
            Console.WriteLine("Day 6 - 2: Orbits Loaded");

            var myOrbit = orbits.FirstOrDefault(o => o.Child == "YOU");
            var myAncestors = myOrbit.GetAncestors();
            var sanOrbit = orbits.FirstOrDefault(o => o.Child == "SAN");
            var sanAncestors = sanOrbit.GetAncestors();

            var sharedAncestor = myAncestors.ToList().Intersect(sanAncestors).Last();

            var transfers = myOrbit.GetDistanceFromAncestor(sharedAncestor)
                         + sanOrbit.GetDistanceFromAncestor(sharedAncestor);

            Console.WriteLine($"Day 6 - 2: {transfers}");
        }


    }
}
