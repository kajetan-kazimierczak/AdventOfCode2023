using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day05
{
    internal class Day05
    {
        class MapItem
        {
            public ulong Destination { get; set; }
            public ulong Source { get; set; }
            public ulong RangeLength { get; set; }
        }

        private List<MapItem> _seedToSoilMap = new List<MapItem>();
        private List<MapItem> _soilToFertilizerMap = new List<MapItem>();
        private List<MapItem> _fertilizerToWaterMap = new List<MapItem>();
        private List<MapItem> _waterToLightMap = new List<MapItem>();
        private List<MapItem> _lightToTemperatureMap = new List<MapItem>();
        private List<MapItem> _temperatureToHumidityMap = new List<MapItem>();
        private List<MapItem> _humidityToLocationMap = new List<MapItem>();
        private List<ulong> _seeds = new List<ulong>();
        private List<List<MapItem>> _maps = new List<List<MapItem>>();
        private bool _ranges = false;


        public void Run()
        {
            var data = "seeds: 79 14 55 13\r\n\r\nseed-to-soil map:\r\n50 98 2\r\n52 50 48\r\n\r\nsoil-to-fertilizer map:\r\n0 15 37\r\n37 52 2\r\n39 0 15\r\n\r\nfertilizer-to-water map:\r\n49 53 8\r\n0 11 42\r\n42 0 7\r\n57 7 4\r\n\r\nwater-to-light map:\r\n88 18 7\r\n18 25 70\r\n\r\nlight-to-temperature map:\r\n45 77 23\r\n81 45 19\r\n68 64 13\r\n\r\ntemperature-to-humidity map:\r\n0 69 1\r\n1 0 69\r\n\r\nhumidity-to-location map:\r\n60 56 37\r\n56 93 4".Split("\r\n");

            data = File.ReadAllLines("Day05\\input.txt");

         //   var ans1 = Part1(data);
            _ranges = true;
            var ans2 = Part2(data);
            Console.WriteLine($"Part 2: {ans2}");
        }

        private ulong Part1(string[] data)
        {
            LoadData(data);
            var Locations = new List<ulong>();
            foreach (var seed in _seeds)
            {
                var dest = seed;
                foreach (var map in _maps)
                {
                    foreach (var mapitem in map)
                    {
                        if (dest >= mapitem.Source && dest < mapitem.Source + mapitem.RangeLength)
                        {
                            dest = dest - mapitem.Source + mapitem.Destination;
                            break;
                        }
                            
                        
                    }
                    
                }
                Locations.Add(dest);
            }
            

            return Locations?.Min() ?? 0;
        }

        private ulong Part2(string[] data)
        {
            LoadData(data);
            var Locations = new List<ulong>();
            var smallest = new List<ulong>();
            var line = data[0];
            var parts = line.Split(" ");
            Console.WriteLine($"Ranges {parts.Length}");
            for (var i = 1; i < parts.Length - 1; i += 2)
            {
                Console.WriteLine($"Range {i}");
                var start = ulong.Parse(parts[i]);
                var range = ulong.Parse(parts[i + 1]);
                var end = start + range - 1;
                for (var j = start; j <= end; j++)
                {
                    _seeds.Add(j);
                }

                foreach (var seed in _seeds)
                {
                    var dest = seed;
                    foreach (var map in _maps)
                    {
                        foreach (var mapitem in map)
                        {
                            if (dest >= mapitem.Source && dest < mapitem.Source + mapitem.RangeLength)
                            {
                                dest = dest - mapitem.Source + mapitem.Destination;
                                break;
                            }


                        }

                    }

                    Locations.Add(dest);
                }
                smallest.Add(Locations.Min());
                Locations.Clear();
                _seeds.Clear();
            }

            return smallest.Min();
        }

        

        private void LoadData(string[] data)
        {
            _seedToSoilMap.Clear();
            _soilToFertilizerMap.Clear();
            _fertilizerToWaterMap.Clear();
            _waterToLightMap.Clear();
            _lightToTemperatureMap.Clear();
            _temperatureToHumidityMap.Clear();
            _humidityToLocationMap.Clear();
            _seeds.Clear();
            _maps.Clear();
            _maps.Add(_seedToSoilMap);
            _maps.Add(_soilToFertilizerMap);
            _maps.Add(_fertilizerToWaterMap);
            _maps.Add(_waterToLightMap);
            _maps.Add(_lightToTemperatureMap);
            _maps.Add(_temperatureToHumidityMap);
            _maps.Add(_humidityToLocationMap);

            var state = 0;
            foreach (var line in data)
            {
                if (line == "")
                {
                    state++;
                    continue;
                }

                switch (state)
                {
                    case 0:
                        LoadSeeds(line);
                        break;
                    case 1:
                        LoadMap(line, _seedToSoilMap);
                        break;
                    case 2:
                        LoadMap(line, _soilToFertilizerMap);
                        break;
                    case 3:
                        LoadMap(line, _fertilizerToWaterMap);
                        break;
                    case 4:
                        LoadMap(line, _waterToLightMap);
                        break;
                    case 5:
                        LoadMap(line, _lightToTemperatureMap);
                        break;
                    case 6:
                        LoadMap(line, _temperatureToHumidityMap);
                        break;
                    case 7:
                        LoadMap(line, _humidityToLocationMap);
                        break;
                }
            }
        }

        private void LoadSeeds(string line)
        {
            var parts = line.Split(" ");

            if (_ranges == false)
            {
                foreach (var part in parts)
                {
                    if (part != "seeds:") _seeds.Add(ulong.Parse(part));
                }
            }
            else
            {
               
            }

        }

        private void LoadMap(string line, List<MapItem> map)
        {
            var parts = line.Split(" ");
            if(parts.Length != 3) return;
            map.Add(new MapItem
            {
                Destination = ulong.Parse(parts[0]),
                Source = ulong.Parse(parts[1]),
                RangeLength = ulong.Parse(parts[2])
            });
        }
    }
}
