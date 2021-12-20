using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._11DumboOctopus
{
	public class Point
	{
		private readonly int x;
		private readonly int y;
		private int energyLevel;
		private bool flashed = false;

		public int X => x;
		public int Y => y;
        public int EnergyLevel { get => energyLevel; set => energyLevel = value; }
        public bool Flashed { get => flashed; set => flashed = value; }

        public Point(int x, int y, int initialEnergyLevel)
		{
			this.x = x;
			this.y = y;
			energyLevel = initialEnergyLevel;
		}
	}
}
