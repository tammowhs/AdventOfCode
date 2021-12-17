using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._08SevenSegmentSearch
{
	public class Signal
	{
		private readonly string originalSignal;
		private readonly string orderedSignal;
		private readonly char[] segments;
		private readonly List<int> possibleValues;

		public string OriginalSignal => originalSignal;
		public string OrderedSignal => orderedSignal;
		public char[] Segments => segments;
		public List<int> PossibleValues => possibleValues;
		public int? Value => PossibleValues.Count == 1 ? possibleValues.First() : null;

		public Signal(string signal)
		{
			originalSignal = signal;
			segments = signal.OrderBy(c => c).ToArray();
			orderedSignal = new string(segments);

			possibleValues = PossibleValuesForLength(segments.Length);
		}

		public void ExcludeValuesFromPossibleValues(IEnumerable<int> nonPossibleValues)
        {
			possibleValues.RemoveAll(pV => nonPossibleValues.Contains(pV));
        }

		private List<int> PossibleValuesForLength(int segmentLength) => segmentLength switch
		{
			2 => new List<int> { 1 },
			3 => new List<int> { 7 },
			4 => new List<int> { 4 },
			5 => new List<int> { 2, 3, 5 },
			6 => new List<int> { 0, 6, 9 },
			7 => new List<int> { 8 },
			_ => throw new ArgumentOutOfRangeException(nameof(segmentLength))
		};

	}
}
