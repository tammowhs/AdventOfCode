using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._03BinaryDiagnostic
{
	public class BinaryDiagnostic : ICodePuzzle
	{
		public int EvaluatePartOne(string input)
		{
//            input = @"00100
//11110
//10110
//10111
//10101
//01111
//00111
//11100
//10000
//11001
//00010
//01010";

			var binaries = DeserializeInput(input);

			var groupedByColumn = GroupByColumn(binaries);

			var mostCommonBitPerCol = groupedByColumn.Select(group => (double)group.Count(b => b == true) / binaries.Count() > 0.5);

			var leastCommonBitPerCol = mostCommonBitPerCol.Select(b => !b);

			var gammaRate = ConvertBinArrToInt(mostCommonBitPerCol);

			var epsilonRate = ConvertBinArrToInt(leastCommonBitPerCol);

			var powerConsumption = gammaRate * epsilonRate;

			return powerConsumption;
		}

		public int EvaluatePartTwo(string input)
        {
//            input = @"00100
//11110
//10110
//10111
//10101
//01111
//00111
//11100
//10000
//11001
//00010
//01010";

            var binaries = DeserializeInput(input);

			Func<int, int, bool> oxygenComparer = (countTrues, countFalses) => countTrues >= countFalses;

			Func<int, int, bool> co2Comparer = (countTrues, countFalses) => countTrues < countFalses;

			var oxygenGeneratorRating = EvaluatePartTwo(binaries, oxygenComparer);

			var co2ScrubberRating = EvaluatePartTwo(binaries, co2Comparer);

			var lifeSupportRating = oxygenGeneratorRating * co2ScrubberRating;

			return lifeSupportRating;
        }

        private int EvaluatePartTwo(List<List<bool>> binaries, Func<int, int, bool> compareCountTruesWithCountFalses)
        {
            var digitsPerBinary = binaries.FirstOrDefault().Count;

            var selection = binaries;

            for (int i = 0; i < digitsPerBinary && selection.Count > 1; i++)
            {
                var bitsAtIndex = selection.Select(bin => bin[i]);

                var numberOfTrues = bitsAtIndex.Count(b => b == true);

                var numberOfFalses = bitsAtIndex.Count(b => b == false);

                var mostCommonValueAtIndex = compareCountTruesWithCountFalses(numberOfTrues, numberOfFalses);

                selection = selection.Where(bin => bin[i] == mostCommonValueAtIndex).ToList();
            }

            var selectedBinArray = selection.Single();

            int selected = ConvertBinArrToInt(selectedBinArray);

            return selected;
        }

        private int ConvertBinArrToInt(IEnumerable<bool> binArray)
        {
            var stringified = string.Join(null, binArray.Select(b => (b == true ? 1 : 0).ToString()));

            var value = Convert.ToInt32(stringified, 2);

            return value;
        }

        private List<List<bool>> GroupByColumn(List<List<bool>> binaries)
		{
			var groupedByColumn = new List<List<bool>>();

			foreach (var binary in binaries)
			{
				for (int indexInBin = 0; indexInBin < binary.Count; indexInBin++)
				{
					if (groupedByColumn.Count <= indexInBin)
					{
						groupedByColumn.Add(new List<bool>());
					}

					var b = binary[indexInBin];

					groupedByColumn[indexInBin].Add(b);
				}
			}

			return groupedByColumn;
		}

		private List<List<bool>> DeserializeInput(string input)
		{
			var binaries = input
				.Split("\r\n")
				.Select(str => str.ToCharArray()
								  //.Select(ch => int.Parse(ch.ToString()))
								  .Select(ch => ch == '1')
								  .ToList()
				)
				.ToList();

			return binaries;
		}
	}
}
