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
            Position = directionAsText.ToUpper() == "H"
                ? Position.Horizontal
                : Position.Vertical;
        }

        public StartPoint(Point point, Position position)
        {
            Point = point;
            Position = position;
        }

        internal Point Point { get; }

        internal Position Position { get; }

        public static StartPoint CreateEmptyStartPoint()
            => new(Point.CreateEmptyPoint(), Position.Horizontal);
    }
}
