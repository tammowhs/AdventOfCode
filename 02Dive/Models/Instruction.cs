using System;

namespace AdventOfCode._02Dive
{
	public class Instruction
	{
		public Directions Direction { get; }
		public int Value { get; }

		public Instruction(Directions direction, int value)
		{
			Direction = direction;
			Value = value;
		}

		public Instruction(string direction, int value) : this(GetDirection(direction), value)
		{
		}

		private static Directions GetDirection(string dir)
		{
			switch (dir)
			{
				case "forward":
					return Directions.Forward;
				case "down":
					return Directions.Down;
				case "up":
					return Directions.Up;
				default:
					throw new Exception();
			}
		}
	}

	public enum Directions
	{
		Forward,
		Down,
		Up
	}
}
