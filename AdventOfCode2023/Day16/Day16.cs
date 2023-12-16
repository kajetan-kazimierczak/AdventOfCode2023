using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day16
{
    internal class Day16
    {
        public void Run()
        {
            var data = ".|...\\....\r\n|.-.\\.....\r\n.....|-...\r\n........|.\r\n..........\r\n.........\\\r\n..../.\\\\..\r\n.-.-/..|..\r\n.|....-|.\\\r\n..//.|....".Split("\r\n");

            data = File.ReadAllLines("Day16\\input.txt");

            //var ans1 = Part1(data);
            var ans2 = Part2(data);
        }

        private object Part2(string[] data)
        {
            var max = 0;

            for (int i = 0; i < data.Length; i++)
            {
                var energized = NumberOfEnergized(data, new Beam(i, 0, 'R'));
                if (energized > max) max = energized;
            }
            for (int i = 0; i < data.Length; i++)
            {
                var energized = NumberOfEnergized(data, new Beam(i, data[0].Length-1, 'L'));
                if (energized > max) max = energized;
            }

            for (int i = 0; i < data[0].Length; i++)
            {
                var energized = NumberOfEnergized(data, new Beam(0, i, 'D'));
                if (energized > max) max = energized;
            }

            for (int i = 0; i < data[0].Length; i++)
            {
                var energized = NumberOfEnergized(data, new Beam(data.Length-1, i, 'U'));
                if (energized > max) max = energized;
            }

            return max;
        }


        private object Part1(string[] data)
        {
            return NumberOfEnergized(data, new Beam(0, 0, 'R'));
        }

        int NumberOfEnergized(string[] data, Beam start)
        {
            var energized = new HashSet<(int Row, int Col)>();
            var queue = new BeamQueue();
            queue.Enqueue(start);

            while (queue.Any())
            {
                var b = queue.Dequeue();

                if (b.Col < 0 || b.Row < 0 || b.Col >= data[0].Length || b.Row >= data.Length) continue;


                var current = data[b.Row][b.Col];


                energized.Add((b.Row, b.Col));

                if (b.Direction == 'R')
                {
                    if (current == '/')
                    {
                        queue.Enqueue(new Beam(b.Row - 1, b.Col, 'U'));
                    }
                    else if (current == '\\')
                    {
                        queue.Enqueue(new Beam(b.Row + 1, b.Col, 'D'));
                    }
                    else if (current == '|')
                    {
                        queue.Enqueue(new Beam(b.Row - 1, b.Col, 'U'));
                        queue.Enqueue(new Beam(b.Row + 1, b.Col, 'D'));
                    }
                    else
                    {
                        queue.Enqueue(new Beam(b.Row, b.Col + 1, 'R'));
                    }
                }
                else if (b.Direction == 'L')
                {
                    if (current == '/')
                    {
                        queue.Enqueue(new Beam(b.Row + 1, b.Col, 'D'));
                    }
                    else if (current == '\\')
                    {
                        queue.Enqueue(new Beam(b.Row - 1, b.Col, 'U'));
                    }
                    else if (current == '|')
                    {
                        queue.Enqueue(new Beam(b.Row - 1, b.Col, 'U'));
                        queue.Enqueue(new Beam(b.Row + 1, b.Col, 'D'));
                    }
                    else
                    {
                        queue.Enqueue(new Beam(b.Row, b.Col - 1, 'L'));
                    }
                }
                else if (b.Direction == 'U')
                {
                    if (current == '/')
                    {
                        queue.Enqueue(new Beam(b.Row, b.Col + 1, 'R'));
                    }
                    else if (current == '\\')
                    {
                        queue.Enqueue(new Beam(b.Row, b.Col - 1, 'L'));
                    }
                    else if (current == '-')
                    {
                        queue.Enqueue(new Beam(b.Row, b.Col + 1, 'R'));
                        queue.Enqueue(new Beam(b.Row, b.Col - 1, 'L'));
                    }
                    else
                    {
                        queue.Enqueue(new Beam(b.Row - 1, b.Col, 'U'));
                    }
                }
                else if (b.Direction == 'D')
                {
                    if (current == '/')
                    {
                        queue.Enqueue(new Beam(b.Row, b.Col - 1, 'L'));
                    }
                    else if (current == '\\')
                    {
                        queue.Enqueue(new Beam(b.Row, b.Col + 1, 'R'));
                    }
                    else if (current == '-')
                    {
                        queue.Enqueue(new Beam(b.Row, b.Col + 1, 'R'));
                        queue.Enqueue(new Beam(b.Row, b.Col - 1, 'L'));
                    }
                    else
                    {
                        queue.Enqueue(new Beam(b.Row + 1, b.Col, 'D'));
                    }
                }
            }

            return energized.Count;
        }

    }
    record Beam(int Row, int Col, char Direction);
    class BeamQueue
    {
        private Queue<Beam> queue = new Queue<Beam>();
        private HashSet<Beam> tried = new HashSet<Beam>();
        public Beam Dequeue()
        {
            return queue.Dequeue();
        }

        public void Enqueue(Beam beam)
        {
            if (!tried.Contains(beam))
            {
                queue.Enqueue(beam);
                tried.Add(beam);
            }
            
        }

        public bool Any()
        {
            return queue.Any();
        }

    }   
}
