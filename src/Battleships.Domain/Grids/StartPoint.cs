namespace Battleships.Domain.Grids
{
    using Battleships.Domain.Common;
    using System;
    using System.Text.RegularExpressions;

    public class StartPoint
    {
        public StartPoint(Point point, string directionAsText)
        {
            if (string.IsNullOrEmpty(directionAsText))
                throw new ArgumentNullException(nameof(directionAsText));

            if (!Regex.Match(directionAsText, RegexPatterns.DirectionPattern).Success)
                throw new ArgumentException(directionAsText, nameof(directionAsText));

            Point = point ?? throw new ArgumentNullException(nameof(point));
            Direction = directionAsText.ToUpper() == "H"
                ? Direction.Horizontal
                : Direction.Vertical;
        }

        public StartPoint(Point point, Direction direction)
        {
            Point = point;
            Direction = direction;
        }

        internal Point Point { get; }

        internal Direction Direction { get; }

        public static StartPoint CreateEmptyStartPoint()
            => new(Point.CreateEmptyPoint(), Direction.Horizontal);
    }
}
