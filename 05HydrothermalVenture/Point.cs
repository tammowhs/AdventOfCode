using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._05HydrothermalVenture
{
    public record Point
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        //public Point(int[] coordinates)
        //{
        //    X = coordinates[0];
        //    Y = coordinates[1];
        //}

        public Point(string coordinatesString)
        {
            var coordinates = coordinatesString.Split(',').Select(c => int.Parse(c)).ToArray();
            X = coordinates[0];
            Y = coordinates[1];
        }
    }
}
