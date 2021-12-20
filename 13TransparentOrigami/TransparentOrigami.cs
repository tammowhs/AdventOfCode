using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._13TransparentOrigami
{
	public class TransparentOrigami : ICodePuzzle
	{
		private readonly string testInput = @"6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7
fold along x=5";

		public int EvaluatePartOne(string input)
		{
			//input = testInput;

			var splitUpInput = input.Split("\r\n\r\n");

			var dots = splitUpInput[0]
						.Split("\r\n")
						.Select(dS => dS.Split(','))
						.Select(dS => new Point(int.Parse(dS[0]), int.Parse(dS[1])))
						.ToList();

			var instructionPrefixLength = "fold along ".Length;
			var foldInstructions = splitUpInput[1]
									.Split("\r\n")
									.Select(instStr => instStr.Substring(instructionPrefixLength))
									.Select(instStr => instStr.Split('='))
									.Select(instr => new Instruction(MapAxis(instr[0]), int.Parse(instr[1])))
									.ToList();

            var instruction = foldInstructions[0];
            dots = FoldDots(dots, instruction);

			return dots.Count;
		}

		public int EvaluatePartTwo(string input)
		{
			//input = testInput;

			var splitUpInput = input.Split("\r\n\r\n");

			var dots = splitUpInput[0]
						.Split("\r\n")
						.Select(dS => dS.Split(','))
						.Select(dS => new Point(int.Parse(dS[0]), int.Parse(dS[1])))
						.ToList();

			var instructionPrefixLength = "fold along ".Length;
			var foldInstructions = splitUpInput[1]
									.Split("\r\n")
									.Select(instStr => instStr.Substring(instructionPrefixLength))
									.Select(instStr => instStr.Split('='))
									.Select(instr => new Instruction(MapAxis(instr[0]), int.Parse(instr[1])))
									.ToList();

			foreach (var instruction in foldInstructions)
			{
				dots = FoldDots(dots, instruction).OrderBy(d => d.X).ThenBy(d => d.Y).ToList();
			}

			// print to console => 5 Letters will be displayed
			
			return dots.Count;
		}

		private Axis MapAxis(string axisChar) => axisChar switch
		{
			"x" => Axis.X,
			"y" => Axis.Y,
			_ => throw new ArgumentOutOfRangeException()
		};

		private List<Point> FoldDots(IEnumerable<Point> dots, Instruction instruction)
		{
			if(instruction.Axis == Axis.Y)
            {
				var dotsToFold = dots.Where(d => d.Y > instruction.Value);

				var foldedDots = dotsToFold.Select(dTF => new Point(dTF.X, instruction.Value - (dTF.Y - instruction.Value)));

				dots = dots.Except(dotsToFold).Concat(foldedDots).Distinct();
            }
			else
            {
				var dotsToFold = dots.Where(d => d.X > instruction.Value);

				var foldedDots = dotsToFold.Select(dTF => new Point(instruction.Value - (dTF.X - instruction.Value), dTF.Y));

				dots = dots.Except(dotsToFold).Concat(foldedDots).Distinct();
			}

			return dots.ToList();
		}
	}

	public enum Axis
    {
		X,
		Y,
    }

	public record Point(int X, int Y);
	public record Instruction(Axis Axis, int Value);
}
