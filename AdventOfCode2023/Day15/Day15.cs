using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day15
{
    internal class Day15
    {
        public void Run()
        {
            var data = "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7".Split("\r\n");

            data = File.ReadAllLines("Day15\\input.txt");

            //var ans1 = Part1(data);
            var ans2 = Part2(data);
        }

        private object Part2(string[] data)
        {
            var input = data[0].Split(',');
            var boxes = new Dictionary<string, int>[256];
            for(int i = 0; i < 256; i++)
            {
                boxes[i] = new Dictionary<string, int>();
            }

            foreach (var lens in input)
            {
                var l = lens.Split('=');
                if (l[0].EndsWith('-'))
                {
                    var name = l[0].Substring(0, l[0].Length - 1);
                    var box = hash(name);
                    if (boxes[box].ContainsKey(name))
                    {
                        boxes[box].Remove(name);
                        boxes[box] = boxes[box].ToList().ToDictionary(); // hack to make dictionary forget old positions
                    }
                    
                }
                else
                {
                    var name = l[0];
                    var box = hash(name);
                    var value = int.Parse(l[1]);

                    // Replace the value if exists. Add otherwise
                    if (boxes[box].ContainsKey(name))
                    {
                        boxes[box][name] = value;
                    }
                    else
                    {
                        boxes[box].Add(name, value);
                    }

                }

                // DebugPrint(boxes, lens);
            }


            var sum = 0;

            for (int i = 0; i < 256; i++)
            {
                if (boxes[i].Count != 0)
                {
                    var box = boxes[i].ToList();
                    for(int j = 0; j < box.Count; j++)
                    {
                        sum += (i + 1) * (j + 1) * box[j].Value;
                    }
                }
            }

            return sum;
            
        }

        private void DebugPrint(Dictionary<string, int>[] boxes, string step)
        {

            Console.WriteLine($"After {step}");

            for (int i = 0; i < 256; i++)
            {
                if (boxes[i].Count != 0)
                {
                    Console.Write($"Box {i} ");

                    foreach (var lens in boxes[i])
                    {
                        Console.Write($"[{lens.Key} {lens.Value}] ");

                    }
                    Console.WriteLine();
                    Console.WriteLine();

                }
               
            }
        }


        private int hash(string input)
        {
            var hash = 0;

            foreach (var c in input)
            {
                hash += c;
                hash *= 17;
                hash = hash % 256;
            }

            return hash;
        }


        private object Part1(string[] data)
        {
            var input = data[0].Split(',');

            var sum = 0;

            foreach (var step in input)
            {
                var hash = 0;

                foreach (var c in step)
                {
                    hash += c;
                    hash *= 17;
                    hash = hash % 256;
                }
                sum += hash;
            }

            return sum;
        }
    }
}
