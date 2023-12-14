using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day14
{
    internal class Day14
    {
        public void Run()
        {
            var data = "O....#....\r\nO.OO#....#\r\n.....##...\r\nOO.#O....O\r\n.O.....O#.\r\nO.#..O.#.#\r\n..O..#O..O\r\n.......O..\r\n#....###..\r\n#OO..#....".Split("\r\n");

            data = File.ReadAllLines("Day14\\input.txt");

            var ans1 = Part1(data);
            var ans2 = Part2(data);
        }

        private object Part2(string[] data)
        {

            var input = data.Select(x => x.ToCharArray()).ToArray();

            var cache = new Dictionary<string, int>();

            while (true)
            {
                var key = GetKey(input);
                if (cache.ContainsKey(key))
                {
                    
                    var list = cache.Keys.ToList();
                    
                    var index = list.IndexOf(key);

                    //foreach (var k in cache)
                    //{
                    //    Console.ForegroundColor = k.Key == key ? ConsoleColor.Red : ConsoleColor.White;
                    //    Console.WriteLine($"{k.Key}  {k.Value}" );
                    //}

                    var loops = 1000000000UL;
     
                    var diff = (ulong)cache.Count - (ulong)index;

                    var rest = ((loops - (ulong)cache.Count) % (ulong)diff);
                    
                    var ans = cache[ list[index + (int)rest] ];

                    return ans;
                }

                var load = GetLoad(input);
                cache.Add(key, load);
                Thumble(input);
                

            }
            

            DebugPrint(input);

            return -1;

        }

        private int GetLoad(char[][] input)
        {
            var weight = 0;
            for (int i = input.Length - 1; i >= 0; i--)
            {

                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == 'O')
                    {
                        weight += (input.Length - i);
                        //            Console.WriteLine($"{new string( input[i])}");
                    }
                }
            }

            return weight;
        }

        private string GetKey(char[][] input)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    sb.Append(input[i][j]);
                }
            }

            return sb.ToString();
        }

        private void Thumble(char[][] input)
        {
            // Move North
            var moved = true;
            while (moved)
            {
                moved = false;

                for (int i = 1; i < input.Length; i++)
                {
                    for (int j = 0; j < input[i].Length; j++)
                    {

                        if (input[i - 1][j] == '.' && input[i][j] == 'O')
                        {
                            input[i - 1][j] = 'O';
                            input[i][j] = '.';
                            moved = true;
                        }
                    }

                }
            }

            // Move West
            moved = true;
            while (moved)
            {
                moved = false;
                for (int i = 0; i < input.Length; i++)
                {
                    for (int j = 1; j < input[i].Length; j++)
                    {
                        if (input[i][j - 1] == '.' && input[i][j] == 'O')
                        {
                            input[i][j - 1] = 'O';
                            input[i][j] = '.';
                            moved = true;
                        }

                    }
                }
            }

            // Move South
            moved = true;
            while (moved)
            {
                moved = false;
                for (int i = input.Length - 2; i >= 0; i--)
                {
                    for (int j = 0; j < input[i].Length; j++)
                    {
                        if (input[i + 1][j] == '.' && input[i][j] == 'O')
                        {
                            input[i + 1][j] = 'O';
                            input[i][j] = '.';
                            moved = true;
                        }

                    }
                }
            }

            // Move East
            moved = true;
            while (moved)
            {
                moved = false;
                for (int i = 0; i < input.Length; i++)
                {
                    for (int j = input[i].Length - 2; j >= 0; j--)
                    {
                        if (input[i][j + 1] == '.' && input[i][j] == 'O')
                        {
                            input[i][j + 1] = 'O';
                            input[i][j] = '.';
                            moved = true;
                        }
                    }
                }
            }
        }

        private object Part1(string[] data)
        {
            var input = data.Select(x => x.ToCharArray()).ToArray();
            
            var moved = true;
            while (moved)
            {
                moved = false;
                
                for (int i = 1; i < input.Length; i++)
                {
                    for (int j = 0; j < input[i].Length; j++)
                    {
                        if (input[i][j] == 'O')
                        {
                            if (input[i - 1][j] == '.' && input[i][j] == 'O')
                            {
                                input[i - 1][j] = 'O';
                                input[i][j] = '.';
                                moved = true;
                            }

                        }
                    }
                }
            }

            var weight = 0;
            for (int i = input.Length - 1; i >= 0; i--)
            {
              
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == 'O')
                    {
                        weight += (input.Length -  i);
            //            Console.WriteLine($"{new string( input[i])}");
                    }
                }
            }

            return weight;

        }

        private void DebugPrint(char[][] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    Console.Write(input[i][j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

   
}
