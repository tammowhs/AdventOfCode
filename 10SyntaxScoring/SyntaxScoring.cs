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
            input = testInput;

            var lines = input.Split("\r\n").Select(value => new Line(value)).ToList();

            var allCoruupted = lines.Where(l => l.CorruptingSymbol.HasValue).ToList();

            return 0;
        }

        public int EvaluatePartTwo(string input)
        {
            throw new NotImplementedException();
        }
    }
}
