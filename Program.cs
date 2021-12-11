using AdventOfCode._02Dive;

namespace AdventOfCode
{
	class Program
	{
		static void Main(string[] args)
		{
			string input = "";

			ICodePuzzle level = new Dive();

			var solution = level.Evaluate(input);
		}
	}
}
