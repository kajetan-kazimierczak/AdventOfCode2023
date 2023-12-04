using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day04
{
    internal class Day04
    {
        public void Run()
        {
            var data = "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53\r\nCard 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19\r\nCard 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1\r\nCard 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83\r\nCard 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36\r\nCard 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11".Split("\r\n");

            data = File.ReadAllLines("Day04\\input.txt");

            var ans1 = Part1(data);
            var ans2 = Part2(data);
        }

        private int Part1(string[] data)
        {

            var ans = 0;

            var cards = new List<Card>();
            foreach (var str in data)
            {
                cards.Add(new Card(str));
            }

            foreach (var card in cards)
            {
                var intersect = card.WinningNumbers.Intersect(card.MyNumbers);
                var a =  1 << (intersect.Count() - 1)  ;
                ans += a;
            }
            

            return ans;
        }

        private int Part2(string[] data)
        {
            var ans = 0;

            var cards = new List<Card>();
            foreach (var str in data)
            {
                cards.Add(new Card(str));
            }

            var lastCardNr = cards.Count;
            for (var i = 1; i <= lastCardNr; i++)
            {
                var currentCards = cards.Where(c => c.Id == i).ToList();
                var currentCard = currentCards.First();
                var newCards = currentCard.WinningNumbers.Intersect(currentCard.MyNumbers).Count();
                
                for (int j = i + 1; j <= i + newCards; j++)
                {
                    for (var k = 0; k < currentCards.Count; k++)
                    {
                        cards.Add(cards.First(c => c.Id == j));
                    }
                    
                }
            }

            ans = cards.Count;
            return ans;
        }

        class Card
        {
            public int Id { get; set; }
            public HashSet<int> WinningNumbers { get; set; } = new HashSet<int>();
            public HashSet<int> MyNumbers { get; set; } = new HashSet<int>();

            public Card(string str)
            {
                var parts = str.Split(":");
                Id = int.Parse(parts[0].Replace("Card ", "").Trim());
                var numbers = parts[1].Split("|");
                foreach (var number in numbers[0].Split(" "))
                {
                    if(!string.IsNullOrEmpty(number))
                        WinningNumbers.Add(int.Parse(number.Trim()));
                }

                foreach (var number in numbers[1].Split(" "))
                {
                    if (!string.IsNullOrEmpty(number))
                        MyNumbers.Add(int.Parse(number.Trim()));
                }
            }   
        }
    }
    
}
