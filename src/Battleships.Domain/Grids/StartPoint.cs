namespace Battleships.Domain.Grids
{
    using Battleships.Domain.Extensions;
    using Battleships.Domain.Ships;
    using Battleships.Domain.Common;
    using System;
    using System.Text.RegularExpressions;

    public class StartPoint
    {
        public StartPoint(Point point, string directionAsText)
        {
            string.IsNullOrEmpty(directionAsText)
                .IfTrue(() => throw new ArgumentNullException(nameof(directionAsText)));

            Regex.Match(directionAsText, RegexPatterns.DirectionPattern).Success
                .IfFalse(() => throw new ArgumentException(directionAsText, nameof(directionAsText)));

            Point = point ?? throw new ArgumentNullException(nameof(point));
            ShipPosition = directionAsText.ToUpper() == "H"
                ? ShipPosition.Horizontal
                : ShipPosition.Vertical;
        }

        public StartPoint(Point point, ShipPosition shipPosition)
        {
            Point = point;
            ShipPosition = shipPosition;
        }

        internal Point Point { get; }

        internal ShipPosition ShipPosition { get; }

        public static StartPoint CreateEmptyStartPoint()
            => new(Point.CreateEmptyPoint(), ShipPosition.Horizontal);
    }
}
