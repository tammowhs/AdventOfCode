using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._08SevenSegmentSearch
{
	public class Display
	{
		private readonly string value;
		private readonly List<Signal> inputSignals;
		private readonly List<Signal> outputSignals;

		public List<Signal> InputSignals => inputSignals;
		public List<Signal> OutputSignals => outputSignals;
		private IEnumerable<Signal> AllSignals => inputSignals.Concat(outputSignals);

		public Display(string value)
		{
			this.value = value;

			var signalsStrings = value.Split(" | ");

			var inputSignalsStrings = signalsStrings[0].Split(' ');
			inputSignals = inputSignalsStrings.Select(iSS => new Signal(iSS)).ToList();

			var outputSignalsStrings = signalsStrings[1].Split(' ');
			outputSignals = outputSignalsStrings.Select(oSS => new Signal(oSS)).ToList();
		}

		public int Evaluate()
		{
			var solvedValues = AllSignals.Where(s => s.Value.HasValue && s.Value != 8);

			var solvedValuesEnumerator = solvedValues.GetEnumerator();

			while (outputSignals.Any(s => !s.Value.HasValue))
			{
				solvedValuesEnumerator.MoveNext();
				
				var knownSignal = solvedValuesEnumerator.Current;

				if(knownSignal is null)
				{
					solvedValuesEnumerator = solvedValues.GetEnumerator();
					continue;
				}

				var containingDigits = DigitsThatContainsValue(knownSignal.Value.Value);

				var inverseContainingDigits = Enumerable.Range(0,10).Except(containingDigits).ToList();

				var digitsContainedByValue = DigitsContainedByValue(knownSignal.Value.Value);

				var inversedDigitsContainedByValue = Enumerable.Range(0, 10).Except(digitsContainedByValue).ToList();

				foreach (var signal in AllSignals.Where(s => !s.Value.HasValue))
				{
                    // Annahme:
                    // 1.) man hat ein unbekanntes Signal 'abcde' von dem man weiß, dass es 2, 3 oder 5 repräsentiert. 
                    // 2.) man weiß, dass in der Sieben Segment Notation 1 in den Ziffern 0,1,3,4,7,8,9 "enthalten" ist.
                    // 3a.) man weiß, dass 1 dem Signal 'ag' entspricht.
                    //		'ag' ist nicht in 'abcde' enthalten, das heißt die möglichen Werte sind 2,3,5 ohne 0,1,3,4,7,8,9
                    //		ergo sind die möglichen Werte 2,5
                    // 3a.) man weiß, dass 1 dem Signal 'ab' entspricht.
                    //		'ab' ist in 'abcde' enthalten, das heißt die möglichen Werte sind 2,3,5 ohne das invertierte von (0,1,3,4,7,8,9)
                    //		ergo 2,3,5 ohne 2,5,6
                    //		ergo sind die möglichen Werte 3

                    // Wenn ein noch unbekanntes (noch mehrere Werte möglich) Signal mit einem bekannten Signal verglichen wird,
                    // und alle Segmente des Bekannten Teil der Segmente des Unbekannten ist, so kann man die möglichen Werte des Unbekannten so weiter einschränken,
                    // sodass diese Werte nur von dem Bekannten ableitbaren Signalen entspricht. // alle anderen rausschmeißen, hier else Fall
                    // Wenn die Segmente des Bekannten nicht vollständig in den Segmenten des Unbekannten ist, kann man die möglichen Werte des Unbekannten so weiter einschränken,
                    // sodass diese Werte keine von dem Bekannten ableitbaren Signalen entspricht. // hier if Fall
                    // Ableitbare Signale von X bedeutet alle Signale deren Segmente die Segmente von X vollständig enthalten. // DigitsThatContainsValue switch-expression unten
                    if (!knownSignal.Segments.IsSubSetOf(signal.Segments))
                    {
                        signal.PossibleValues.RemoveAll(pV => containingDigits.Contains(pV));
                    }
                    else
                    {
                        signal.PossibleValues.RemoveAll(pV => inverseContainingDigits.Contains(pV));
                    }

					// Annahme:
					// 1.) man hat ein bekanntes Signal 'abdefg' von dem man weiß, dass es 6 repräsentiert. 
					// 2.) man weiß, dass 6 aus den Ziffern 5,6 abgeleitet werden kann (switch-expression DigitsContainedByValue)
					// 3a.) man weiß, dass das unbekannte Signal 'abdfg' 2,5 entspricht.
					//		'abdfg' ist in 'abdefg' enthalten, das heißt die möglichen Werte sind 2,5 ohne das invertierte von 5,6
					//		ergo 2,5 ohne 0,1,2,3,4,7,8,9
					//		ergo sind die möglichen Werte 5
					// 3b.) man weiß, dass das unbekannte Signal 'acdeg' 2,5 entspricht.
					//		'acdeg' ist nicht in 'abdefg' enthalten, das heißt die möglichen Werte sind 2,5 ohne 5,6
					//		ergo sind die möglichen Werte 2
					if (signal.Segments.IsSubSetOf(knownSignal.Segments))
					{
						signal.PossibleValues.RemoveAll(pV => inversedDigitsContainedByValue.Contains(pV));
					}
					else
					{
						signal.PossibleValues.RemoveAll(pV => digitsContainedByValue.Contains(pV));
					}

					if (signal.Value.HasValue)
					{
						var signalValue = signal.Value.Value;

						var allSignalsWithSameSignalString = AllSignals.Where(s => s.OrderedSignal == signal.OrderedSignal).ToList();

						allSignalsWithSameSignalString.ForEach(s =>
							{
								s.PossibleValues.Clear();
								s.PossibleValues.Add(signalValue);
							});
					}
				}
			}

			int pin = 0;
			foreach (var signal in outputSignals)
			{
				pin *= 10;
				pin += signal.Value.Value;
			}

			return pin;
		}

		private List<int> DigitsThatContainsValue(int value) => value switch
		{
			0 => new List<int> { 0, 8 },
			1 => new List<int> { 0, 1, 3, 4, 7, 8, 9 },
			2 => new List<int> { 2, 8 },
			3 => new List<int> { 3, 8, 9 },
			4 => new List<int> { 4, 8, 9 },
			5 => new List<int> { 5, 6, 8, 9 },
			6 => new List<int> { 6, 8 },
			7 => new List<int> { 0, 3, 7, 8, 9 },
			8 => new List<int> { 8 },
			9 => new List<int> { 8, 9 },
			_ => throw new ArgumentOutOfRangeException(nameof(value))
		};

		private List<int> DigitsContainedByValue(int value) => value switch
		{
			0 => new List<int> { 0, 1, 7 },
			1 => new List<int> { 1 },
			2 => new List<int> { 2 },
			3 => new List<int> { 1, 3 },
			4 => new List<int> { 1, 4 },
			5 => new List<int> { 5 },
			6 => new List<int> { 5, 6 },
			7 => new List<int> { 1, 7 },
			8 => new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 },
			9 => new List<int> { 1, 3, 4, 5, 7, 9 },
			_ => throw new ArgumentOutOfRangeException(nameof(value))
		};
	}
}
