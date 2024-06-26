﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._05HydrothermalVenture
{
	public class HydrothermalVenture : ICodePuzzle
	{
		public int EvaluatePartOne(string input)
        {
            //            input = @"0,9 -> 5,9
            //8,0 -> 0,8
            //9,4 -> 3,4
            //2,2 -> 2,1
            //7,0 -> 7,4
            //6,4 -> 2,0
            //0,9 -> 2,9
            //3,4 -> 1,4
            //0,0 -> 8,8
            //5,5 -> 8,2";

            var lines = DeserializeInput(input).ToList();
            var pointsOfMultipleOcurrence = FindDuplicates(lines);

            return pointsOfMultipleOcurrence.Count;

        }

        public int EvaluatePartTwo(string input)
        {
            var lines = DeserializeInput(input, true);
            var pointsOfMultipleOcurrence = FindDuplicates(lines);

            return pointsOfMultipleOcurrence.Count;
        }

        private IEnumerable<Line> DeserializeInput(string input, bool considerDiagonals = false)
        {
            var lines = input.Split("\r\n")
                .Select(str => new Line(str));

            if(!considerDiagonals)
                return lines.Where(line => line.IsNonDiagonally);

            return lines;
        }

        private List<Point> FindDuplicates(IEnumerable<Line> lines)
        {
            var allPoints = lines.SelectMany(line => line.GetAllCoveredPoints());

            var pointsOfMultipleOcurrence = allPoints
                .GroupBy(p => p)
                .Where(group => group.Count() > 1)
                .SelectMany(group => group)
                .Distinct()
                .ToList();

            return pointsOfMultipleOcurrence;

            // zu inperformant, da geht bei großen Datensets mein Rechner in die Knie :D

            //var pointsOfMultipleOcurrences = allPoints.Where(p => allPoints.Count(ap => ap == p) > 1);
            //var distinctPointsOfMultipleOcurrences = pointsOfMultipleOcurrences.Distinct();
            //return distinctPointsOfMultipleOcurrences.Count();
        }

        
	}
}
