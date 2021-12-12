using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._06Lanternfish
{
    public class Fish
    {
        public int Timer { get; private set; }

        public Fish(int timer)
        {
            Timer = timer;
        }

        public Fish() : this(8)
        {
        }

        public void NewDay()
        {
            if (Timer == 0)
                Timer = 7;

            if (Timer > 0)
                Timer--;
        }
    }
}
