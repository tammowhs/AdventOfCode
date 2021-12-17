using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._08SevenSegmentSearch
{
	public class SevenSegmentSearch : ICodePuzzle
	{
		private readonly string testInput = @"be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe
edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc
fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg
fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb
aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea
fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb
dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe
bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef
egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb
gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce";

		public int EvaluatePartOne(string input)
		{
			//input = testInput;

			var displays = input.Split("\r\n").Select(d => new Display(d)).ToList();

			var outputSignals = displays.SelectMany(d => d.OutputSignals).ToList();

			var countUniqueDigits = outputSignals.Where(s => s.Value.HasValue).Count();

			return countUniqueDigits;
		}

		public int EvaluatePartTwo(string input)
		{
			//input = testInput;

			var displays = input.Split("\r\n").Select(d => new Display(d)).ToList();

			var decodedDisplayOutputs = displays.Select(d => d.Evaluate()).ToList();

			var sumOfDecodedDisplayOutputs = decodedDisplayOutputs.Sum();

			return sumOfDecodedDisplayOutputs;
		}
	}
}
