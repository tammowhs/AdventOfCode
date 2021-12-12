using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._01SonarSweep
{
	public class SonarSweep : ICodePuzzle
	{
		public int EvaluatePartOne(string input)
		{
			var depths = input.Split("\r\n").Select(depthAsString => int.Parse(depthAsString)).ToList();

			var countIncreases = CountIncreases(depths);

			return countIncreases;
		}

		public int EvaluatePartTwo(string input)
		{
			var depths = input.Split("\r\n").Select(depthAsString => int.Parse(depthAsString)).ToList();

			var measurementSlidingWindowSums = GetMeasurementSlidingWindowSums(depths, 3);

			var countIncreases = CountIncreases(measurementSlidingWindowSums);

			return countIncreases;
		}

		private List<int> GetMeasurementSlidingWindowSums(List<int> depths, int windowSize)
		{
			var measurementSlidingWindowSums = new List<int>();

			for (var i = 0; i < depths.Count - windowSize + 1; i++)
			{
				var sum = depths.GetRange(i, windowSize).Sum();

				measurementSlidingWindowSums.Add(sum);
			}

			return measurementSlidingWindowSums;
		}

		private int CountIncreases(List<int> depths)
		{
			var countIncreases = 0;

			for (var i = 1; i < depths.Count; i++)
			{
				if (depths[i] > depths[i - 1])
				{
					countIncreases++;
				}
			}

			return countIncreases;
		}
	}
}
