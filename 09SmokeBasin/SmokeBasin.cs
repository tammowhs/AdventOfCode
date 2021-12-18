using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._09SmokeBasin
{
	public class SmokeBasin : ICodePuzzle
	{
		private readonly string testInput = @"2199943210
3987894921
9856789892
8767896789
9899965678";

		public int EvaluatePartOne(string input)
        {
            input = testInput;

            IEnumerable<Point> points = DeserializeInputString(input).ToArray();

            IEnumerable<Point> allLowest = MarkAndFindAllLowest(points);

            var heights = allLowest.Select(p => p.Height);

            var sumHeights = heights.Sum();

            var solution = sumHeights + allLowest.Count();

            return solution;
        }

        public int EvaluatePartTwo(string input)
        {
            input = testinput;

            var points = DeserializeInputString(input).ToArray();

            var allLowest = MarkAndFindAllLowest(points).ToList();

            var basins = allLowest.Select(lowest => FindBasinForPoint(points, lowest));

            var largestBasins = basins
                                    .Select(basin => basin.Count())
                                    .OrderByDescending(count => count)
                                    .Take(3);

            var productOfLargestBasins = largestBasins.Aggregate(1, (acc, value) => acc * value);

            return productOfLargestBasins;
        }

        private IEnumerable<Point> FindBasinForPoint(Point[] points, Point point)
        {
            var adjacents = FindAdjacents(points, point).ToList();
            // var oneUpAdjacents = adjacents.Where(aP => aP.Height == point.Height + 1);
            // basins dont need to be continuous, so a.e. 0 can come from 1, but also from 2 3 4 5 6 7 8
            var oneUpAdjacents = adjacents.Where(aP => aP.Height > point.Height && aP.Height != 9);

            if (point.Height == 8 || !oneUpAdjacents.Any())
                return new List<Point> { point };

            var adjacentsOfAdjacents = oneUpAdjacents.Select(oUAP => FindBasinForPoint(points, oUAP));

            var all = adjacentsOfAdjacents
                        .SelectMany(aOA => aOA)
                        .Concat(new List<Point> { point })
                        .GroupBy(p => new { p.X, p.Y })
                        .Select(group => group.First());

            return all;
        }

        private static IEnumerable<Point> DeserializeInputString(string input)
        {
            return input.Split("\r\n")
                        .Select((rowString, yIndex) =>
                            rowString
                                .ToCharArray()
                                .Select((heightChar, xIndex) =>
                                    new Point(xIndex, yIndex, int.Parse(heightChar.ToString()))
                                )
                        ).SelectMany(r => r);
        }

        private IEnumerable<Point> MarkAndFindAllLowest(IEnumerable<Point> points)
        {
            foreach (var point in points)
            {
                var adjacents = FindAdjacents(points, point).ToList();

                if (adjacents.All(adjacents => adjacents.Height > point.Height))
                {
                    point.IsLowest = true;
                }
            }

            var allLowest = points.Where(p => p.IsLowest);

            return allLowest;
        }

        private IEnumerable<Point> FindAdjacents(IEnumerable<Point> points, Point point)
        {
			return points.Where(p => (p.Y == point.Y && p.X >= point.X - 1 && p.X <= point.X + 1)
								  || (p.X == point.X && p.Y >= point.Y - 1 && p.Y <= point.Y + 1))
						.Where(p => !(p.X == point.X && p.Y == point.Y));
        }
	}
}
