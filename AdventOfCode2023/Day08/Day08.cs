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

        private ulong Part2(string[] data)
        {
          
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


        //    private object Part2(string[] data)
        //    {
        //        var graph = ParseInput(data);
        //        var steps = CountStepsToZ(graph);
        //        return steps;
        //    }

        //    Dictionary<string, Node> ParseInput(string[] input)
        //    {
        //        var graph = new Dictionary<string, Node>();
        //        foreach (var line in input)
        //        {
        //            var parts = line.Split(' ');
        //            var node = new Node { Name = parts[0] };
        //            node.Left = graph.ContainsKey(parts[1]) ? graph[parts[1]] : new Node { Name = parts[1] };
        //            node.Right = graph.ContainsKey(parts[2]) ? graph[parts[2]] : new Node { Name = parts[2] };
        //            graph[node.Name] = node;
        //        }
        //        return graph;
        //    }
        //    int CountStepsToZ(Dictionary<string, Node> graph)
        //    {
        //        var queue = new Queue<IEnumerable<Node>>();
        //        var startNodes = graph.Values.Where(n => n.Name.EndsWith("A"));
        //        queue.Enqueue(startNodes);

        //        int steps = 0;
        //        while (queue.Any())
        //        {
        //            var currentNodes = queue.Dequeue();
        //            steps++;

        //            // Check if all nodes end with 'Z'
        //            if (currentNodes.All(n => n.Name.EndsWith("Z")))
        //            {
        //                return steps;
        //            }

        //            // Add next nodes to the queue
        //            var nextNodes = currentNodes.SelectMany(n => new[] { n.Left, n.Right });
        //            queue.Enqueue(nextNodes);
        //        }
        //        return steps;
        //    }
        //}
        //class Node
        //{
        //    public string Name;
        //    public Node Left;
        //    public Node Right;
        //}
    }
}
