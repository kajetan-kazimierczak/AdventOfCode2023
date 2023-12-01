using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day01
{
    internal class Day01
    {
        

        public void Run()
        {
           var data = System.IO.File.ReadAllLines(@"Day01\input.txt");

            //var data = "two1nine\r\neightwothree\r\nabcone2threexyz\r\nxtwone3four\r\n4nineeightseven2\r\nzoneight234\r\n7pqrstsixteen".Split("\r\n");

            var ans1 = Part1(data);
            var ans2 = Part2(data);
        }

        private int Part1(string[] data)
        {
            var input = data.Select(x => ToDigits(x));
            var sum = input.Select(x => int.Parse(x[0].ToString() + x[^1])).Sum();

            return sum;
        }

        private int Part2(string[] data)
        {

            var sum = data.Select(x => int.Parse(FirstDigit(x) + LastDigit(x))).Sum();

            return sum;
           
        }

        private static string FirstDigit(string str)
        {
            string[] digits =
            {
                "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "one", "two", "three", "four", "five", "six", "seven",
                "eight", "nine"
            };
    
            var res = string.Empty;
            var min = int.MaxValue;
            for (int i = 0; i < digits.Length; i++)
            {
                var index = str.IndexOf(digits[i]);
                if (index != -1 && index < min)
                {
                    min = index;
                    res = digits[i];
                }
            }
            res = res.Replace("one", "1").Replace("two", "2").Replace("three", "3").Replace("four", "4").Replace("five", "5").Replace("six", "6").Replace("seven", "7").Replace("eight", "8").Replace("nine", "9");
            return res;
        }

        private static string LastDigit(string str)
        {
            string[] digits =
            {
                "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "one", "two", "three", "four", "five", "six", "seven",
                "eight", "nine"
            };
         
            var res = string.Empty;
            var max = int.MinValue;
            for (int i = 0; i < digits.Length; i++)
            {
                var index = str.LastIndexOf(digits[i]);
                if (index != -1 && index > max)
                {
                    max = index;
                    res = digits[i];
                }
            }
            res = res.Replace("one", "1").Replace("two", "2").Replace("three", "3").Replace("four", "4").Replace("five", "5").Replace("six", "6").Replace("seven", "7").Replace("eight", "8").Replace("nine", "9");
            return res;
        }

        private static string ToDigits(string str)
        {
            var res = string.Empty;
            if (string.IsNullOrEmpty(str)) return string.Empty;

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] - '0' >= 0 && str[i] - '0' <= 9) res += str[i];
            }

            return res;
        }
    }
}
