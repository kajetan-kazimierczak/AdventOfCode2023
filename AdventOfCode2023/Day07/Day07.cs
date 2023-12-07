using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day07
{
    internal class Day07
    {
        public void Run()
        {
            var data = "32T3K 765\r\nT55J5 684\r\nKK677 28\r\nKTJJT 220\r\nQQQJA 483".Split("\r\n");

            data = File.ReadAllLines("Day07\\input.txt");

            var ans1 = Part1(data);

            var ans2 = Part2(data);

        }

        private int Part1(string[] data)
        {
            // sort hands
            var changed = false;
            do
            {
                changed = false;
                for (var i = 0; i < data.Length - 1; i++)
                {
                    var hand1 = data[i].Substring(0, 5);
                    var hand2 = data[i + 1].Substring(0, 5);
                    if (CompareHands(hand1, hand2) > 0)
                    {
                        var temp = data[i];
                        data[i] = data[i + 1];
                        data[i + 1] = temp;
                        changed = true;
                    }
                }
            } while (changed);

            // multiply hand bid with rank
            var ans = 0;
            for (var i = 0; i < data.Length; i++)
            {
                var bid = int.Parse(data[i].Substring(6));
                ans += bid * (i + 1);
                // Console.WriteLine($"{data[i]} {i+1} {bid} {bid * (i + 1)}");
            }

            return ans;
        }

        private int HandValue(string hand)
        {
            var cards = "23456789TJQKA";

            // create and indexed array of card => count
            var handCounts = new int[13];

            for (var i = 0; i < 5; i++)
            {
                var card = hand.Substring(i, 1);
                handCounts[cards.IndexOf(card)]++;
            }

            var handValue = 0;

            // 5 of a kind
            if (handCounts.Any(x => x == 5)) return 10;
            // 4 of a kind
            if (handCounts.Any(x => x == 4)) return 9;
            // full house
            if (handCounts.Any(x => x == 3) && handCounts.Any(x => x == 2)) return 8;
            // 3 of a kind
            if (handCounts.Any(x => x == 3)) return 7;
            // 2 pair
            if (handCounts.Count(x => x == 2) == 2) return 6;
            // 1 pair
            if (handCounts.Any(x => x == 2)) return 5;
            // high card
            return 0;

        }

        private int CompareHands(string hand1, string hand2)
        {

            var cards = "23456789TJQKA";

            var hand1Value = HandValue(hand1);
            var hand2Value = HandValue(hand2);

            var comp = hand1Value.CompareTo(hand2Value);
            if (comp != 0) return comp;
            for (int i = 0; i < 5; i++)
            {
                var card1 = hand1.Substring(i, 1);
                var card2 = hand2.Substring(i, 1);
                comp = cards.IndexOf(card1).CompareTo(cards.IndexOf(card2));
                if (comp != 0) return comp;
            }

            return 0;
        }


        private object Part2(string[] data)
        {
            // sort hands
            var changed = false;
            do
            {
                changed = false;
                for (var i = 0; i < data.Length - 1; i++)
                {
                    var hand1 = data[i].Substring(0, 5);
                    var hand2 = data[i + 1].Substring(0, 5);
                    if (CompareHands2(hand1, hand2) > 0)
                    {
                        var temp = data[i];
                        data[i] = data[i + 1];
                        data[i + 1] = temp;
                        changed = true;
                    }
                }
            } while (changed);

            // multiply hand bid with rank
            var ans = 0;
            for (var i = 0; i < data.Length; i++)
            {
                var bid = int.Parse(data[i].Substring(6));
                ans += bid * (i + 1);
                // Console.WriteLine($"{data[i]} {i+1} {bid} {bid * (i + 1)}");
            }

            return ans;
        }


        private int HighestHandValue(string hand)
        {
            var cards = "J23456789TQKA";

            var highest = 0;
            foreach (var c in cards)
            {
                var newhand = hand.Replace('J', c);
                var value = HandValue2(newhand);
                if (value > highest) highest = value;
            }

            return highest;
        }

        private int HandValue2(string hand)
        {
            var cards = "J23456789TQKA";

            // create and indexed array of card => count
            var handCounts = new int[13];

            for (var i = 0; i < 5; i++)
            {
                var card = hand.Substring(i, 1);
                handCounts[cards.IndexOf(card)]++;
            }

            var handValue = 0;

            // 5 of a kind
            if (handCounts.Any(x => x == 5)) return 10;
            // 4 of a kind
            if (handCounts.Any(x => x == 4)) return 9;
            // full house
            if (handCounts.Any(x => x == 3) && handCounts.Any(x => x == 2)) return 8;
            // 3 of a kind
            if (handCounts.Any(x => x == 3)) return 7;
            // 2 pair
            if (handCounts.Count(x => x == 2) == 2) return 6;
            // 1 pair
            if (handCounts.Any(x => x == 2)) return 5;
            // high card
            return 0;

        }

        private int CompareHands2(string hand1, string hand2)
        {

            var cards = "J23456789TQKA";

            var hand1Value = HighestHandValue(hand1);
            var hand2Value = HighestHandValue(hand2);

            var comp = hand1Value.CompareTo(hand2Value);
            if (comp != 0) return comp;
            for (int i = 0; i < 5; i++)
            {
                var card1 = hand1.Substring(i, 1);
                var card2 = hand2.Substring(i, 1);
                comp = cards.IndexOf(card1).CompareTo(cards.IndexOf(card2));
                if (comp != 0) return comp;
            }

            return 0;
        }

    }
}
