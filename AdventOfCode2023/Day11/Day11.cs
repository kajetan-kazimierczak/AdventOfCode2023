using System.Diagnostics;

namespace AdventOfCode2023.Day11
{
    internal class Day11
    {
        public void Run()
        {
            var data = "...#......\r\n.......#..\r\n#.........\r\n..........\r\n......#...\r\n.#........\r\n.........#\r\n..........\r\n.......#..\r\n#...#.....".Split("\r\n");

            data = File.ReadAllLines("Day11\\input.txt");

            var ans1 = Part1(data);
            var ans2 = Part2(data);
        }

        private object Part2(string[] data)
        {
            var oldGalaxies = GetGalaxies(data);

            var galaxies = ExpandSpace(oldGalaxies, data, 1000000);

            var ans = 0UL;

            for (int i = 0; i < galaxies.Count - 1; i++)
            {
                for (int j = i + 1; j < galaxies.Count; j++)
                {
                    var dist = Math.Abs(galaxies[i].Row - galaxies[j].Row) + Math.Abs(galaxies[i].Col - galaxies[j].Col);

                    ans += (ulong)dist;
                }
            }

            return ans;

        }

        private object Part1(string[] data)
        {
            var oldGalaxies = GetGalaxies(data);
            var galaxies = ExpandSpace(oldGalaxies, data, 2);

            var ans = 0;

            for (int i = 0; i < galaxies.Count - 1; i++)
            {
                for (int j = i+1; j < galaxies.Count; j++)
                {
                    var dist = Math.Abs(galaxies[i].Row - galaxies[j].Row) + Math.Abs(galaxies[i].Col - galaxies[j].Col);
                    
                    ans += dist;
                }
            }
      
            return ans;
     
        }

        private List<Galaxy> GetGalaxies(string[] data)
        {
            var galaxies = new List<Galaxy>();

            for (int row = 0; row < data.Length; row++)
            {
                for (int col = 0; col < data[row].Length; col++)
                {
                    if (data[row][col] == '#')
                    {
                        galaxies.Add(new Galaxy { Row = row, Col = col });
                    }
                }
            }

            return galaxies;
        }


        private List<Galaxy> ExpandSpace(List<Galaxy> galaxies, string[] space, int size )
        {
            var newGalaxies = galaxies.Select(x => new Galaxy { Col = x.Col, Row = x.Row }).ToList();

            var emptyRows = new List<int>();
            var emptyCols = new List<int>();

            for (int col = space[0].Length - 1; col >= 0; col--)
            {
                var isEmpty = true;
                for (int row = 0; row < space.Length; row++)
                {
                    if (space[row][col] == '#')
                    {
                        isEmpty = false;
                        break;
                    }
                }

                if (isEmpty)
                {
                    foreach (var galaxy in newGalaxies.Where(x => x.Col >= col))
                    {
                        galaxy.Col += size - 1 ;
                    }
                }
            }
            for (int row = space.Length - 1; row >= 0; row--)
            {
                if (!space[row].Contains('#'))
                {
                    foreach (var galaxy in newGalaxies.Where(x => x.Row >= row))
                    {
                        galaxy.Row += size - 1;
                    }
                }
            }
            return newGalaxies;
        }


        [DebuggerDisplay("Row: {Row}, Col: {Col}")]
        class Galaxy
        {
            public int Row { get; set; }
            public int Col { get; set; }
        }
    }
}
