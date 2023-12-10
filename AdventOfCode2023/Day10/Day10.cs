using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day10
{
    internal class Day10
    {
        private string[] _data;
        public void Run()
        {
            var data = ".F----7F7F7F7F-7....\r\n.|F--7||||||||FJ....\r\n.||.FJ||||||||L7....\r\nFJL7L7LJLJ||LJ.L-7..\r\nL--J.L7...LJS7F-7L7.\r\n....F-J..F7FJ|L7L7L7\r\n....L7.F7||L7|.L7L7|\r\n.....|FJLJ|FJ|F7|.LJ\r\n....FJL-7.||.||||...\r\n....L---J.LJ.LJLJ...".Split("\r\n");

             data = File.ReadAllLines("Day10\\input.txt");

            _data = data;

           // var ans1 = Part1(data);

            var ans2 = Part2(data);
            Console.WriteLine($"Tiles: {ans2}");
        }

        private object Part2(string[] data)
        {
            

            var positions = LoadPositions(data);

            var p = positions.Find(x => x.Shape == 'S');
            p.Shape = GetRealStartShape(p, positions);

            if(positions.Any(x => x.Shape == 'S')) throw new Exception();

            var tilesCount = 0;

            

            for (var row = 0; row < data.Length; row++)
            {
                var inside = false;
                bool currentlyInUpperCorner = false;
                bool currentlyInLoweCorner = false;


                for (var col = 0; col < data[row].Length; col++)
                {

                    var c = data[row][col];
                    var currentPosition = positions.Find(p => p.Row == row && p.Col == col);

                    currentlyInUpperCorner = currentlyInUpperCorner || currentPosition?.Shape == 'F';
                    if (currentlyInUpperCorner && currentPosition?.Shape == '7')
                    {
                        currentlyInUpperCorner = false;
                    } else if (currentlyInUpperCorner && currentPosition?.Shape == 'J')
                    {
                        currentlyInUpperCorner = false;
                        inside = !inside;
                    }

                    
                    currentlyInLoweCorner = currentlyInLoweCorner || currentPosition?.Shape == 'L';
                    if (currentlyInLoweCorner && currentPosition?.Shape == 'J')
                    {
                        currentlyInLoweCorner = false;
                    } else if (currentlyInLoweCorner && currentPosition?.Shape == '7')
                    {
                        currentlyInLoweCorner = false;
                        inside = !inside;
                    }

                    

                    if (inside && data[row][col] == '.')
                    {
                        tilesCount++;
                    }
                    if (currentPosition?.Shape == '|')
                    {
                        inside = !inside;
                    }
                    //else if ()
                    //{
                    //    inside = true;
                    //}

                    DebugPrint(data[row][col], inside, currentPosition);
                }
                Console.WriteLine();
            }

            return tilesCount;
        }

        void DebugPrint(char c, bool inside, Position currentPos)
        {
            Console.BackgroundColor = ConsoleColor.Black;
             Dictionary<char, char> NiceShapes = new Dictionary<char, char>()
            {
                {'F', '╔'},
                {'7', '╗'},
                {'J', '╝'},
                {'L', '╚'},
                {'|', '║'},
                {'-', '═'},
                {'S', 'S'},
                {'I', 'I'},
                {'O', 'O'},
            };
            if (c != '.')
            {
                Console.ForegroundColor = currentPos == null ? (inside ? ConsoleColor.DarkGreen : ConsoleColor.DarkRed) : inside ? ConsoleColor.Cyan: ConsoleColor.White;
                Console.Write(NiceShapes[currentPos?.Shape ?? c]);
            }
            else if (c == '.')
            {
                Console.BackgroundColor = inside ? ConsoleColor.Green : ConsoleColor.Red;
                Console.Write(inside ? 'I' : 'O');
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        //List<Position> CreateOrderedListOfCorners(List<Position> positions)
        //{
        //    var corners = new List<Position>();

        //    var current = positions.First();
        //    //corners.Add(current);
        //    var direction = 'L';
        //    var tmp = positions[1];

        //    direction = tmp.Row == current.Row ? (tmp.Col > current.Col ? 'L' : 'R') : ( tmp.Row > current.Row ? 'D' : 'U' );

        //    while (!corners.Any(x => x.Col == tmp.Col && x.Row == tmp.Row))
        //    {
        //        if (current.Shape == 'S')
        //        {
        //            corners.Add(current);
        //        }
        //        if (current.Shape == 'F')
        //        {
        //            direction = direction switch { 'L' => 'D', 'U' => 'R', _ => throw new Exception() };
        //            corners.Add(current);
        //        }
        //        if (current.Shape == '7')
        //        {
        //            direction = direction switch { 'R' => 'D', 'U' => 'L', _ => throw new Exception() };
        //            corners.Add(current);
        //        }
        //        if (current.Shape == 'J')
        //        {
        //            direction = direction switch { 'R' => 'U', 'D' => 'L', _ => throw new Exception() };
        //            corners.Add(current);
        //        }
        //        if (current.Shape == 'L')
        //        {
        //            direction = direction switch { 'L' => 'U', 'D' => 'L', _ => throw new Exception() };
        //            corners.Add(current);
        //        }
                
        //        if (direction == 'L')
        //        {
        //            var next = positions.Where(x => x.Row == current.Row).OrderBy(x => x.Col).First(x => x.Col > current.Col);
        //            corners.Add(next);
        //            current = next;
        //        }
        //        else if (direction == 'R')
        //        {
        //            var next = positions.Where(x => x.Row == current.Row).OrderByDescending(x => x.Col).First(x => x.Col < current.Col);
        //            corners.Add(next);
        //            current = next;
        //        }
        //        else if (direction == 'U')
        //        {
        //            var next = positions.Where(x => x.Col == current.Col).OrderByDescending(x => x.Row).First(x => x.Row < current.Row);
        //            corners.Add(next);
        //            current = next;
        //        }
        //        else if (direction == 'D')
        //        {
        //            var next = positions.Where(x => x.Col == current.Col).OrderBy(x => x.Row).First(x => x.Row > current.Row);
        //            corners.Add(next);
        //            current = next;
        //        }
        //    }
           
        //    return corners;
        //}

        //bool IsPointInPolygon(float x, float y, List<Position> corners)
        //{
        //    var polyCorners = corners.Count;
        //    var polyY = corners.Select(x => x.Row).ToArray();
        //    var polyX = corners.Select(x => x.Col).ToArray();
        //    int i, j = polyCorners - 1;
        //    bool oddNodes = false;

        //    for (i = 0; i < polyCorners; i++)
        //    {
        //        if (polyY[i] < y && polyY[j] >= y
        //            || polyY[j] < y && polyY[i] >= y)
        //        {
        //            if (polyX[i] + (y - polyY[i]) / (polyY[j] - polyY[i]) * (polyX[j] - polyX[i]) < x)
        //            {
        //                oddNodes = !oddNodes;
        //            }
        //        }
        //        j = i;
        //    }

        //    return oddNodes;
        //}

        //private object Part1(string[] data)
        //{
            
        //    var positions = LoadPositions(data);

        //    var ans = positions.OrderByDescending(p => p.Distance).First().Distance;

        //    return ans;
        //}


        private char GetRealStartShape(Position p, List<Position> positions)
        {
            var canGoLeft = positions.Any(x => x.Col == p.Col - 1 && x.Row == p.Row && (x.Shape == '-' || x.Shape == 'F' ||x.Shape == 'L'));
            var canGoRight = positions.Any(x => x.Col == p.Col + 1 && x.Row == p.Row && (x.Shape == '-' || x.Shape == '7' || x.Shape == 'J'));
            var canGoUp = positions.Any(x => x.Col == p.Col && x.Row == p.Row - 1 && (x.Shape == '|' || x.Shape == 'F' || x.Shape == '7'));
            var canGoDown = positions.Any(x => x.Col == p.Col && x.Row == p.Row + 1 && (x.Shape == '|' || x.Shape == 'J' || x.Shape == 'L'));

            if (canGoLeft && canGoRight) return '-';
            if (canGoUp && canGoDown) return '|';
            if (canGoLeft && canGoDown) return '7';
            if (canGoLeft && canGoUp) return 'J';
            if (canGoRight && canGoDown) return 'F';
            if (canGoRight && canGoUp) return 'L';

            return 'S';
        }

        private List<Position> LoadPositions(string[] data)
        {
            var positions = new List<Position>();

            // find S
            for (var row = 0; row < data.Length; row++)
            {
                for (var col = 0; col < data[row].Length; col++)
                {
                    if (data[row][col] == 'S')
                    {
                        positions.Add(new Position() { Row = row, Col = col, Shape = 'S', Distance = 0 });
                    }
                }
            }

            // find all positions

            var q = new Queue<Position>();
            q.Enqueue(positions[0]); // S
            var steps = 0;
            while (q.Any())
            {
                var current = q.Dequeue();


                var newPositions = PossibleMovesFrom(current.Row, current.Col);

                foreach (var newPosition in newPositions)
                {
                    if (positions.Any(p => p.Col == newPosition.Col && p.Row == newPosition.Row)) { continue; }

                    newPosition.Distance = (char)(current.Distance + 1);
                    positions.Add(newPosition);
                    q.Enqueue(newPosition);
                }

            }

            return positions;
        }

        private List<Position> PossibleMovesFrom(int row, int col)
        {
            var symbol = _data[row][col];

            var pos = new List<Position>();

            

            if (symbol == 'S')
            {
                // up
                if ("|F7".Contains(_data[row - 1][col]))
                {
                    pos.Add(new Position { Col = col, Row = row - 1, Shape = _data[row - 1][col] });
                }

                // down
                if ("|JL".Contains(_data[row + 1][col]))
                {
                    pos.Add(new Position { Col = col, Row = row + 1, Shape = _data[row + 1][col] });
                }

                // left
                if ("-LF".Contains(_data[row][col - 1]))
                {
                    pos.Add(new Position { Col = col - 1, Row = row, Shape = _data[row][col - 1] });
                }

                // right
                if ("-J7".Contains(_data[row][col + 1]))
                {
                    pos.Add(new Position { Col = col + 1, Row = row, Shape = _data[row][col + 1] });
                }
                // no pipe
                return pos;
            }

            if (symbol == '-')
            {
                pos.Add(new Position { Col = col - 1, Row = row, Shape = _data[row][col - 1] });
                pos.Add(new Position { Col = col + 1, Row = row, Shape = _data[row][col + 1] });
                return pos;
            }

            if (symbol == '|')
            {
                pos.Add(new Position { Col = col, Row = row - 1, Shape = _data[row - 1][col] });
                pos.Add(new Position { Col = col, Row = row + 1, Shape = _data[row + 1][col] });
                return pos;
            }

            if (symbol == 'F')
            {
                pos.Add(new Position { Col = col + 1, Row = row, Shape = _data[row][col + 1] });
                pos.Add(new Position { Col = col, Row = row + 1, Shape = _data[row + 1][col] });
                return pos;
            }

            if (symbol == '7')
            {
                pos.Add(new Position { Col = col - 1, Row = row, Shape = _data[row][col - 1] });
                pos.Add(new Position { Col = col, Row = row + 1, Shape = _data[row + 1][col] });
                return pos;
            }
            if (symbol == 'J')
            {
                pos.Add(new Position { Col = col, Row = row - 1, Shape = _data[row - 1][col] });
                pos.Add(new Position { Col = col - 1, Row = row, Shape = _data[row][col - 1] });
                return pos;
            }
            if (symbol == 'L')
            {
                pos.Add(new Position { Col = col, Row = row - 1, Shape = _data[row - 1][col] });
                pos.Add(new Position { Col = col + 1, Row = row, Shape = _data[row][col + 1] });
                return pos;
            }

            return pos;
        }

        [DebuggerDisplay("({Shape}: {Row},{Col}")]
        class Position
        {
           
            public int Row { get; set; }
            public int Col { get; set; }
            public char Shape { get; set; }
            public int Distance { get; set; }
            

        }
    }
}
