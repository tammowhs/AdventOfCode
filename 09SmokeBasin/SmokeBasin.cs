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
			//input = testInput;

			var points = input
				.Split("\r\n")
				.Select((rowString, yIndex) =>
					rowString
						.ToCharArray()
						.Select((heightChar, xIndex) =>
							new Point(xIndex, yIndex, int.Parse(heightChar.ToString()))
						)
				).SelectMany(r => r)
				.ToList();

            foreach (var point in points)
            {
				var adjacents = FindAdjacents(points, point).ToList();

				if(adjacents.All(adjacents => adjacents.Height > point.Height))
                {
					point.IsLowest = true;
                }
            }

			var allLowest = points.Where(p => p.IsLowest);

			var heights = allLowest.Select(p => p.Height);

			var sumHeights = heights.Sum();

			var solution = sumHeights + allLowest.Count();

			return solution;
		}

		private IEnumerable<Point> FindAdjacents(IEnumerable<Point> points, Point point)
        {
			return points.Where(p => (p.Y == point.Y && p.X >= point.X - 1 && p.X <= point.X + 1)
								  || (p.X == point.X && p.Y >= point.Y - 1 && p.Y <= point.Y + 1))
						.Where(p => !(p.X == point.X && p.Y == point.Y));
        }

		public int EvaluatePartTwo(string input)
		{
			throw new NotImplementedException();
		}
	}
}
