using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._04GiantSquid
{
    public class Block
    {
        public int ID { get; }
        public bool IsBingo { get; set; }
        public List<Field> Fields { get; }

        public Block(int iD, List<Field> fields)
        {
            ID = iD;
            Fields = fields;
        }

    }
}
