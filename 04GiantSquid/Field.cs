using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._04GiantSquid
{
    public class Field
    {
        public int Value { get; }
        public int Row { get; }
        public int Col { get; }
        public int Block { get; }
        public bool Marked { get; set; }

        public Field(int value, int row, int col, int block)
        {
            Value = value;
            Row = row;
            Col = col;
            Block = block;
            Marked = false;
        }
    }
}
