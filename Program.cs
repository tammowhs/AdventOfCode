﻿using AdventOfCode._02Dive;
using AdventOfCode._03BinaryDiagnostic;
using AdventOfCode._04GiantSquid;
using AdventOfCode._05HydrothermalVenture;
using System;

namespace AdventOfCode
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = "";

			ICodePuzzle level = new HydrothermalVenture();

            var solution1 = level.EvaluatePartOne(input);

            Console.WriteLine(solution1);

            var solution2 = level.EvaluatePartTwo(input);

            Console.WriteLine(solution2);
        }
	}
}
