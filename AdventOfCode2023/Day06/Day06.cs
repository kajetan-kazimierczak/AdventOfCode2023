using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day06
{
    internal class Day06
    {
        public void Run()
        {
            var data = "Time:      7  15   30\r\nDistance:  9  40  200".Split("\r\n");

            data = File.ReadAllLines("Day06\\input.txt");

            var ans1 = Part1(data);
            
            var ans2 = Part2(data);
            
        }

        private ulong Part1(string[] data)
        {
            //int[] times = { 7, 15, 30 };
            //int[] distances = {9,40,200};

            //int[] times = { 71530 };
            //int[] distances = { 940200 };

            //int[] times = { 47, 98, 66, 98 };
            //int[] distances = { 400, 1213, 1011, 1540 };

            ulong[] times = { 47986698 };
            ulong[] distances = { 400121310111540 };

            ulong ans = 1;
            for (var i = 0; i < times.Length; i++)
            {
                var time = times[i];
                var distance = distances[i];
                var wins = 0UL;
                for (ulong j = 0; j < time; j++)
                {
                    var distanceTraveled =  j * (time - j);
                    if (distanceTraveled > distance)
                    {
                        wins++;
                    }
                }
                ans = ans * wins;
            }

            return ans;
        }

        private int Part2(string[] data)
        {
            throw new NotImplementedException();
        }
    }
}
