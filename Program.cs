using AdventOfCode._02Dive;
using AdventOfCode._03BinaryDiagnostic;
using System;

namespace AdventOfCode
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = "";

			ICodePuzzle level = new BinaryDiagnostic();

            var solution1 = level.EvaluatePartOne(input);

            Console.WriteLine(solution1);

            var solution2 = level.EvaluatePartTwo(input);

			Console.WriteLine(solution2);
		}
	}
}
