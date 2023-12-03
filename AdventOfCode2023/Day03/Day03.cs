using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day03
{
    internal class Day03
    {
        public void Run()
        {
            var data = "467..114..\r\n...*......\r\n..35..633.\r\n......#...\r\n617*......\r\n.....+.58.\r\n..592.....\r\n......755.\r\n...$.*....\r\n.664.598..".Split("\r\n");

             data = File.ReadAllLines("Day03\\input.txt");

            var ans1 = Part1(data);
            var ans2 = Part2(data);
        }

        private int Part2(string[] data)
        {
            var sum = 0;
            var parts = new List<Part>();
            var gears = new List<Part>();
            char[] numbers = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

            for (var row = 0; row < data.Length; row++)
            {
                var current = string.Empty;
                var startCol = -1;

                for (var col = 0; col < data[row].Length; col++)
                {
                    
                    if (numbers.Contains(data[row][col]))
                    {
                        current += data[row][col];
                        if (startCol == -1)
                        {
                            startCol = col;
                        }
                    }
                    else
                    {
                        if (data[row][col] == '*') gears.Add(new Part(row, col, 1, 0));

                        if (!string.IsNullOrEmpty(current) && startCol >= 0)
                        {
                            parts.Add(new Part(row, startCol, current.Length, int.Parse(current)));
                            current = string.Empty;
                            startCol = -1;
                        }
                    }
                    
                }

                if (!string.IsNullOrEmpty(current) && startCol >= 0)
                {
                    parts.Add(new Part(row, startCol, current.Length, int.Parse(current)));
                    current = string.Empty;
                    startCol = -1;
                }

            }

            foreach (var gear in gears)
            {
                var adjacentParts = parts.Where(p => 
                        p.Row == gear.Row - 1 && p.Col >= gear.Col  - p.Length && p.Col <= gear.Col + 1 || // check above
                        p.Row == gear.Row + 1 && p.Col >= gear.Col  - p.Length && p.Col <= gear.Col + 1 || // check below
                        p.Row == gear.Row && p.Col == gear.Col + 1 || // check right
                        p.Row == gear.Row && p.Col == gear.Col - p.Length // check left
                    ).ToList();
                if (adjacentParts.Count == 2)
                {
                    sum += adjacentParts[0].Value * adjacentParts[1].Value;
                }
            }

            return sum;
        }

        record Part(int Row, int Col, int Length, int Value);
        

        private int Part1(string[] data)
        {
            var sum = 0;
            char[] numbers = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            
            for (var row = 0; row < data.Length; row++)
            {
                var current = string.Empty;
                var isPart = false;
                for (var col = 0; col < data[row].Length; col++)
                {

                    if (numbers.Contains(data[row][col]))
                    {
                        current += data[row][col];

                        int cols = data.Length - 1;
                        int rows = data[0].Length - 1;

                        if (
                            (row > 0 && col > 0 && data[row - 1][col - 1] != '.') || // check upper left
                            (row > 0 && data[row - 1][col] != '.') || // check top
                            (row > 0 && col < cols && data[row - 1][col + 1] != '.') || // check upper right

                            (col > 0 && data[row][col - 1] != '.') && !numbers.Contains(data[row][col - 1]) || // check left)
                            (col < cols && data[row][col + 1] != '.') && !numbers.Contains(data[row][col + 1]) || // check right

                            (row < rows && col > 0 && data[row + 1][col - 1] != '.') || // check lower left
                            (row < rows && data[row + 1][col] != '.') || // check bottom
                            (row < rows && col < cols && data[row + 1][col + 1] != '.') // check lower right
                        )
                        {
                            isPart = true;
                        }

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(current) && isPart)
                        {
                            sum += int.Parse(current);
                      
                        }
                        current = string.Empty;
                        isPart = false;
                    }


                }
                if (!string.IsNullOrEmpty(current) && isPart)
                {
                    sum += int.Parse(current);
                    current = string.Empty;
                    isPart = false;
                }

            }
            return sum;
        }
    }
}
