using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._09SmokeBasin
{
	public class Point
	{
		private readonly int x;
		private readonly int y;
		private readonly int height;
		private bool isLowest = false;

		public int X => x;
		public int Y => y;
		public int Height => height;
		public bool IsLowest { get => isLowest; set => isLowest = value; }

		public Point(int x, int y, int height)
		{
			this.x = x;
			this.y = y;
			this.height = height;
		}
	}
}
