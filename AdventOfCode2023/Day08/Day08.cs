using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdventOfCode2023.Day08
{
    internal class Day08
    {
        public void Run()
        {
            var data =
                "RL\r\n\r\nAAA = (BBB, CCC)\r\nBBB = (DDD, EEE)\r\nCCC = (ZZZ, GGG)\r\nDDD = (DDD, DDD)\r\nEEE = (EEE, EEE)\r\nGGG = (GGG, GGG)\r\nZZZ = (ZZZ, ZZZ)"
                    .Split("\r\n");
            data = "LLR\r\n\r\nAAA = (BBB, BBB)\r\nBBB = (AAA, ZZZ)\r\nZZZ = (ZZZ, ZZZ)".Split("\r\n");
            data =
                "LR\r\n\r\n11A = (11B, XXX)\r\n11B = (XXX, 11Z)\r\n11Z = (11B, XXX)\r\n22A = (22B, XXX)\r\n22B = (22C, 22C)\r\n22C = (22Z, 22Z)\r\n22Z = (22B, 22B)\r\nXXX = (XXX, XXX)"
                    .Split("\r\n");
            data = File.ReadAllLines("Day08\\input.txt");

            // var ans1 = Part1(data);

            var ans2 = Part2(data);
            Console.WriteLine($"Steps: {ans2}");

        }

        private object Part1(string[] data)
        {
            var steps = data[0];
            var instructions = data.Skip(2).Select(x => new
                { Name = x.Substring(0, 3), L = x.Substring(7, 3), R = x.Substring(12, 3) }).ToList();

            var numSteps = 0;
            var current = instructions.First(x => x.Name == "AAA");
            while (true)
            {
                if (steps[numSteps % steps.Length] == 'L')
                {
                    current = instructions.First(x => x.Name == current.L);
                }
                else
                {
                    current = instructions.First(x => x.Name == current.R);
                }

                numSteps++;
                if (current.Name == "ZZZ") break;
            }

            return numSteps;

        }


        class Instruction
        {
            public string Name { get; set; }
            public string L { get; set;  }
            public string R { get; set; }
            public int First { get; set; }
            public int Rep { get; set; }
        }

        private ulong Part2(string[] data)
        {
            var steps = data[0];
            ulong stepsLength = ulong.Parse(steps.Length.ToString());
            var instructions = data.Skip(2).Select(x => new Instruction
                { Name = x.Substring(0, 3), L = x.Substring(7, 3), R = x.Substring(12, 3), First = -1, Rep = -1 }).ToList();

            var starts = instructions.Where(x => x.Name.EndsWith('A')).ToList();

            var z = new List<(int first,int rep)>();

            foreach (var s in starts)
            {
                var a = FindStepsToZ(instructions, steps, s.Name);
                z.Add(a);
            }

            var gcd = z.Select(x => (ulong)x.rep).ToList().Aggregate(GCD);

            var t = z.Select(x => (ulong)x.rep / gcd).ToList();

            var q = t.Aggregate((x, y) => x * y);

            var ans = q * gcd;

            return ans;
        }

        private (int first, int rep) FindStepsToZ(
            List<Instruction> instructions, string steps, string start)
        {
            int stepsLength = int.Parse(steps.Length.ToString());
            var current = start;
            var step = 0;
            while (true)
            {
                
                var i = instructions.Find(x => x.Name == current);
                if (i.First == -1)
                {
                    i.First = step;
                }
                else
                {
                    i.Rep = step - i.First;
                    if (i.Name.EndsWith('Z')) 
                        break;
                }

                var a = instructions.FindAll(x => x.Name.EndsWith('Z'));

                if (steps[(int)(step % stepsLength)] == 'L')
                {
                    current =  i.L;
                }
                else
                {
                    current = i.R;
                }

                step++;
            }

            return (instructions.Find(x => x.Name == current).First, instructions.Find(x => x.Name == current).Rep);
        }

     

        static ulong GCD(ulong a, ulong b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }

        private ulong Part2_bruteforce_takes_forever(string[] data)
        {
            var steps = data[0];
            ulong stepsLength = ulong.Parse(steps.Length.ToString());
            var instructions = data.Skip(2).Select(x => new
                { Name = x.Substring(0, 3), L = x.Substring(7, 3), R = x.Substring(12, 3) }).ToList();

            var numSteps = 0UL;
            var current = instructions.Where(x => x.Name.EndsWith('A')).ToList();
            while (true)
            {
                if (steps[(int)(numSteps % stepsLength)] == 'L')
                {
                    current = instructions.Where(x => current.Select(c => c.L).Contains(x.Name)).ToList();
                }
                else
                {
                    current = instructions.Where(x => current.Select(c => c.R).Contains(x.Name)).ToList();
                }

                numSteps++;
                if (current.All(x => x.Name.EndsWith('Z'))) break;
                if (numSteps % 100000 == 0) Console.WriteLine(numSteps);
            }

            return numSteps;
        }


  
    }
}
