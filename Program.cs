﻿using AdventOfCode._02Dive;
using AdventOfCode._03BinaryDiagnostic;
using AdventOfCode._04GiantSquid;
using AdventOfCode._05HydrothermalVenture;
using AdventOfCode._06Lanternfish;
using AdventOfCode._07TheTreacheryOfWhale;
using AdventOfCode._08SevenSegmentSearch;
using System;

namespace AdventOfCode
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = @"";

			ICodePuzzle level = new SevenSegmentSearch();

			var solution1 = level.EvaluatePartOne(input);

			Console.WriteLine(solution1);

            var solution2 = level.EvaluatePartTwo(input);

            Console.WriteLine(solution2);
        }
	}
}
