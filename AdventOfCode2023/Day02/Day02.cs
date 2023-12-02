using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day02
{
    internal class Day02
    {
        public void Run()
        {
            var data = "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green\r\nGame 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue\r\nGame 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red\r\nGame 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red\r\nGame 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green".Split( "\r\n" );
            
            data = File.ReadAllLines( "Day02\\input.txt" );

            var config = "12 red, 13 green, 14 blue";
            var ans1 = Part1( data, config );
            var ans2 = Part2( data, config );
        }

        private object Part2(string[] data, string config)
        {
            var games = new List<Game>();
            var c = new Bag(config);

            foreach (var game in data)
            {
                games.Add(new Game(game));
            }

            var pow = new List<int>();
            foreach (var game in games)
            {
                var lowest = int.MaxValue;
                var pow_lowest = 0;
                var highest_blue = 0;
                var highest_green = 0;
                var highest_red = 0;
                foreach (var bag in game.Bags)
                {
                    if (bag.Red > highest_red) highest_red = bag.Red;
                    if (bag.Green > highest_green) highest_green = bag.Green;
                    if (bag.Blue > highest_blue) highest_blue = bag.Blue;
                }

                pow.Add(highest_red * highest_blue * highest_green);
            }

            return pow.Sum();
        }

        private int Part1(string[] data, string config)
        {
            var games = new List<Game>();
            var c = new Bag( config );

            foreach (var game in data)
            {
                games.Add( new Game( game ) );
            }

            var ans = 0;
            foreach (var game in games)
            {
                if(!game.Bags.Any(x => x.Blue > c.Blue || x.Green > c.Green || x.Red > c.Red)) ans+= game.Number;
            }

            return ans;
        }

    }

    internal class Game
    {
        public Game(string content)
        {
            var g = content.Split( ':' );
            var b = g[1].Split( ';' );
            Number = int.Parse(g[0].Split( ' ')[1]);
            Bags = new List<Bag>();
            foreach (var bag in b)
            {
                Bags.Add( new Bag( bag ) );
            }
        }

        public int Number { get; set; }
        public List<Bag> Bags { get; set; }
    }

    internal class Bag
    {
        public Bag(string content)
        {
            var cubes = content.Split( ',' );
            foreach (var cube in cubes)
            {
                var parts = cube.Trim().Split( ' ' );
                var color = parts[1];
                var count = int.Parse(parts[0]);
                switch (color)
                {
                    case "red":
                        Red += count;
                        break;
                    case "green":
                        Green += count;
                        break;
                    case "blue":
                        Blue += count;
                        break;
                }
            }
        }

        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
    }
}
