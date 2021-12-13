using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._08SevenSegmentSearch
{
    public class Signal
    {
        private readonly string signal;
        private int? value;

        public string OriginalSignal => signal;
        public int? Value { get => value; set => this.value = value; }

        public Signal(string signal)
        {
            this.signal = signal;
            this.Value = TryMap(signal.Length);
        }

        public int? TryMap(int length)
        {
            switch (length)
            {
                // cases are number of segments used in a seven segment display of the corresponding return value
                case 2:
                    return 1;
                case 4:
                    return 4;
                case 3:
                    return 7;
                case 7:
                    return 8;
                default:
                    return null;
            }
        }

    }
}
