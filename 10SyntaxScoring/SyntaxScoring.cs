using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._10SyntaxScoring
{
	public class SyntaxScoring : ICodePuzzle
	{
		private readonly string testInput = @"[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]";

		public int EvaluatePartOne(string input)
		{
			//input = testInput;

			var lines = input.Split("\r\n").Select(value => new Line(value)).ToList();

			var allCorrupted = lines.Where(l => l.CorruptingSymbol.HasValue).ToList();

			var rating = allCorrupted.Select(line => RateSymbolPartOne(line.CorruptingSymbol.Value)).Sum();

			return rating;
		}

		private int RateSymbolPartOne(Symbol symbol) => symbol switch
		{
			Symbol.RoundClose => 3,
			Symbol.SquareClose => 57,
			Symbol.CurlyClose => 1197,
			Symbol.AngleClose => 25137,
			_ => throw new ArgumentException()
		};

		public int EvaluatePartTwo(string input)
		{
            input = testInput;

            var lines = input.Split("\r\n").Select(value => new Line(value)).ToList();

			var allUncorrupted = lines.Where(l => !l.CorruptingSymbol.HasValue).ToList();

			var rating = allUncorrupted
				.Select(line =>
					line.SymbolsToCompleteSequence
						.Select(completingSymbol => RateSymbolPartTwo(completingSymbol))
						.Aggregate(0, (agg, next) => agg * 5 + next)
				)
				.OrderBy(r => r)
				.ToList();

			var middleRating = rating[rating.Count / 2];

			return 0;
		}

		private int RateSymbolPartTwo(Symbol symbol) => symbol switch
		{
			Symbol.RoundClose => 1,
			Symbol.SquareClose => 2,
			Symbol.CurlyClose => 3,
			Symbol.AngleClose => 4,
			_ => throw new ArgumentException()
		};
	}
}
