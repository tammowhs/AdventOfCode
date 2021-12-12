using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._05HydrothermalVenture
{
    public class Line
    {
        public Point From { get; }
        public Point To { get; }

        public bool IsNonDiagonally => IsHorizontal || IsVertical;
        private bool IsDiagonally => !IsNonDiagonally;
        private bool IsHorizontal => From.Y == To.Y;
        private bool IsVertical => From.X == To.X;

        public Line(Point from, Point to)
        {
            From = from;
            To = to;
        }

        public Line(string lineInput)
        {
            var points = lineInput.Trim().Split(" -> ");

            From = new Point(points[0]);
            To = new Point(points[1]); 
        }

        public IEnumerable<Point> GetAllCoveredPoints()
        {
            if (IsHorizontal) // To and From have the same Y Value
            {
                var differenceX = To.X - From.X;

                var signFactor = differenceX < 0 ? -1 : 1;

                var maxIterations = Math.Abs(differenceX) + 1;

                for (int i = 0; i < maxIterations; i++)
                {
                    yield return new Point(From.X + i * signFactor, From.Y);
                }
            }
            else if(IsVertical) // To and From have the same X Value
            {
                var differenceY = To.Y - From.Y;

                var signFactor = differenceY < 0 ? -1 : 1;

                var maxIterations = Math.Abs(differenceY) + 1;

                for (int i = 0; i < maxIterations; i++)
                {
                    yield return new Point(From.X, From.Y + i * signFactor);
                }
            }
            else if(IsDiagonally) // To and From are on a 45° line
            {
                var differenceX = To.X - From.X;
                var signFactorX = differenceX < 0 ? -1 : 1;

                var differenceY = To.Y - From.Y;
                var signFactorY = differenceY < 0 ? -1 : 1;

                var maxIterations = Math.Abs(differenceX) + 1;

                for (int i = 0; i < maxIterations; i++)
                {
                    yield return new Point(From.X + i * signFactorX, From.Y + i * signFactorY);
                }
            }


        }
    }
}
