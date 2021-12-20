using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._11DumboOctopus
{
	public class DumboOctopus : ICodePuzzle
	{
		private readonly string testInput = @"7313511551
3724855867
2374331571
4438213437
6511566287
6727245532
3736868662
2348138263
2417483121
8812617112";

		public int EvaluatePartOne(string input)
		{
			input = testInput;

			var octopusses = DeserializeInputString(input).ToList();

            for (int i = 0; i < 100; i++)
            {
				octopusses = DoStep(octopusses);
            }

			return flashes;
		}

		public int EvaluatePartTwo(string input)
		{
			input = testInput;

			var octopusses = DeserializeInputString(input).ToList();
			int counter = 0;

			while(true)
			{
				octopusses = DoStep(octopusses);
				counter++;

				if(octopusses.All(oct => oct.Flashed))
                {
					return counter;
                }
			}

			throw new Exception();
		}

		private static IEnumerable<Point> DeserializeInputString(string input)
		{
			return input.Split("\r\n")
						.Select((rowString, yIndex) =>
							rowString
								.ToCharArray()
								.Select((initialLevel, xIndex) =>
									new Point(xIndex, yIndex, int.Parse(initialLevel.ToString()))
								)
						).SelectMany(r => r);
		}

		private int flashes = 0;

        private List<Point> DoStep(List<Point> initalStepPoints)
		{
			initalStepPoints.ForEach(p => p.EnergyLevel++);
			initalStepPoints.ForEach(p => p.Flashed = false);

			IEnumerable<Point> octopussesToFlash(IEnumerable<Point> points) => points
			   .Where(p => p.EnergyLevel > 9 && !p.Flashed);

			while (octopussesToFlash(initalStepPoints).Any())
			{
				var octsFlashingInThisStep = octopussesToFlash(initalStepPoints).ToList();
				octsFlashingInThisStep.ForEach(p => p.Flashed = true);
				flashes += octsFlashingInThisStep.Count;


				var adjacentsOfOctsFlashingInThisStep = octsFlashingInThisStep.Select(flashedOct => FindAdjacents(initalStepPoints, flashedOct));

				var unflashedAdjacents = adjacentsOfOctsFlashingInThisStep
					.SelectMany(aOA => aOA)
					//.GroupBy(p => new { p.X, p.Y })
					//.Select(group => group.First())
					.Where(p => !p.Flashed)
					.OrderBy(p => p.Y)
					.ThenBy(p => p.X)
					.ToList();

				unflashedAdjacents.ForEach(p => p.EnergyLevel++);
			}

			initalStepPoints.Where(p => p.EnergyLevel > 9).ToList().ForEach(p => p.EnergyLevel = 0);

			return initalStepPoints;
		}

		private IEnumerable<Point> FindAdjacents(IEnumerable<Point> points, Point point)
		{
			return points.Where(p => (p.X >= point.X - 1 && p.X <= point.X + 1 && p.Y >= point.Y - 1 && p.Y <= point.Y + 1))
						.Where(p => !(p.X == point.X && p.Y == point.Y));
		}
	}
}
