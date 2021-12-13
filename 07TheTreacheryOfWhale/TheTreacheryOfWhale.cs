using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._07TheTreacheryOfWhale
{
	public class TheTreacheryOfWhale : ICodePuzzle
	{
		public int EvaluatePartOne(string input)
		{
			//input = "16,1,2,0,4,2,7,1,2,14";

			var positions = input.Split(',').Select(t => int.Parse(t)).ToArray();

			return EvaluateMinCost(positions, (difference) => difference);
		}

		public int EvaluatePartTwo(string input)
		{
			//input = "16,1,2,0,4,2,7,1,2,14";

			var positions = input.Split(',').Select(t => int.Parse(t)).ToArray();

			return EvaluateMinCost(positions, CalcFuelConsumption);
		}

		private int EvaluateMinCost(int[] positions, Func<int, int> ratingSelector)
		{
			var maxPosition = positions.Max();

			int[] costs = new int[maxPosition];

			for (int i = 0; i < maxPosition; i++)
			{
				var differences = positions.Select(p => Math.Abs(p - i));

				differences = differences.Select(d => ratingSelector(d));

				costs[i] = differences.Sum();
			}

			var lowestCost = costs.Min();

			//var indexOfLowestCost = costs.ToList().FindIndex(c => c == lowestCost);

			//var x = positions[indexOfLowestCost];

			return lowestCost;
		}

		private int CalcFuelConsumption(int steps)
		{
			if (steps == 0)
				return 0;

			var numbers = Enumerable.Range(1, steps);

			var aggregate = numbers.Aggregate((sum, next) => sum + next);

			return aggregate;
		}
	}
}
