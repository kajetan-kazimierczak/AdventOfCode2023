using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day09
{
    internal class Day09
    {
        public void Run()
        {
            var data =
                "0 3 6 9 12 15\r\n1 3 6 10 15 21\r\n10 13 16 21 30 45".Split("\r\n");

           data = File.ReadAllLines("Day09\\input.txt");

            var ans1 = Part1(data);

              var ans2 = Part2(data);
        }

        private object Part2(string[] data)
        {
            var sum = 0;
            foreach (var row in data)
            {
                var values = row.Split(" ").Select(int.Parse).Reverse().ToArray();
                var p = Prediction(values);
                sum += p;
            }

            return sum;
        }

        private object Part1(string[] data)
        {

            var sum = 0;
            foreach (var row in data)
            {
                var values = row.Split(" ").Select(int.Parse).ToArray();
                var p = Prediction(values);
                sum += p;
            }

            return sum;

        }

        private int Prediction(int[] values)
        {
            var differences = new List<List<int>>();
            differences.Add(values.ToList());

            while (differences.Last().Any(x => x != 0))
            {
                var sequence = new List<int>();
                var previous = differences.Last();
                for (int i = 0; i < previous.Count - 1; i++)
                {
                    var diff = previous[i + 1] - previous[i];
                    sequence.Add(diff);
                }
                differences.Add(sequence);
            }

            var pred = 0;
            for (int i = differences.Count - 2; i >= 0; i--)
            {
                pred = differences[i].Last() + pred;
            }

            return pred;
        }
    }
}
