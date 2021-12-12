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
            if(IsHorizontal) // To and From have the same Y Value
            {
                var pointWithGreaterX = From.X >= To.X ? From : To;
                var pointWithSmallerX = From.X >= To.X ? To : From;

                var difference = pointWithGreaterX.X - pointWithSmallerX.X;

                while(difference >= 0)
                {
                    yield return new Point(pointWithGreaterX.X - difference, From.Y);
                    difference--;
                }
            }
            else if(IsVertical) // To and From have the same X Value
            {
                var pointWithGreaterY = From.Y >= To.Y ? From : To;
                var pointWithSmallerY = From.Y >= To.Y ? To : From;

                var difference = pointWithGreaterY.Y - pointWithSmallerY.Y;

                while (difference >= 0)
                {
                    yield return new Point(From.X, pointWithGreaterY.Y - difference);
                    difference--;
                }
            }


        }
    }
}
