using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._02Dive
{
	public class Dive : ICodePuzzle
	{
		public int EvaluatePartOne(string input)
		{
			var instructions = input.Split("\r\n")
				.Select(inst => inst.Split(" "))
				.Select(inst => new Instruction(inst[0], int.Parse(inst[1]))).ToList();

			var product = GetPartOneProduct(instructions);

			return product;
		}

        public int EvaluatePartTwo(string input)
        {
			var instructions = input.Split("\r\n")
				.Select(inst => inst.Split(" "))
				.Select(inst => new Instruction(inst[0], int.Parse(inst[1]))).ToList();

			var product = GetPartTwoProduct(instructions);

			return product;
		}

        private int GetPartOneProduct(List<Instruction> instructions)
		{
			var position = 0;
			var depth = 0;

			foreach (var instruction in instructions)
			{
				switch (instruction.Direction)
				{
					case Directions.Forward:
						position += instruction.Value;
						break;
					case Directions.Down:
						depth += instruction.Value;
						break;
					case Directions.Up:
						depth -= instruction.Value;
						break;
					default:
						throw new Exception();
				}
			}

			var product = depth * position;
			return product;
		}

		private int GetPartTwoProduct(List<Instruction> instructions)
		{
			var position = 0;
			var depth = 0;
			var aim = 0;

			foreach (var instruction in instructions)
			{
				switch (instruction.Direction)
				{
					case Directions.Forward:
						position += instruction.Value;
						depth += aim * instruction.Value;
						break;
					case Directions.Down:
						aim += instruction.Value;
						break;
					case Directions.Up:
						aim -= instruction.Value;
						break;
					default:
						throw new Exception();
				}
			}

			var product = depth * position;
			return product;
		}
	}
}
