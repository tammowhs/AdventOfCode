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

        public Display(string value)
        {
            this.value = value;

            var signalsStrings = value.Split(" | ");

            var inputSignalsStrings = signalsStrings[0].Split(' ');
            inputSignals = inputSignalsStrings.Select(iSS => new Signal(iSS)).ToList();

            var outputSignalsStrings = signalsStrings[1].Split(' ');
            outputSignals = outputSignalsStrings.Select(oSS => new Signal(oSS)).ToList();
        }

    }
}
