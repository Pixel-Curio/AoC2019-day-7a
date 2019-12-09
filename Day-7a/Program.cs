using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_7a
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] code = File.ReadAllText(@"day7a-input.txt").Split(",").Select(int.Parse).ToArray();

            //code = new[] { 3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,
            //    1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0 };

            int highestThrust = 0;

            IEnumerable<IEnumerable<int>> permutations = GetPermutations(Enumerable.Range(0, 5), 5);
            foreach (var inputs in permutations)
            {
                int output = 0;
                foreach (var input in inputs)
                {
                    Intcode processor = new Intcode(code);
                    var results = processor.Process(new List<int> { input, output });
                    output = results[0];
                }

                if (output > highestThrust) highestThrust = output;
            }

            Console.WriteLine($"Highest thrust achieved: {highestThrust}");
        }

        static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}
