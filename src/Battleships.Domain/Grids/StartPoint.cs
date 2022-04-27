namespace Battleships.Domain.Grids
{
    using Battleships.Domain.Extensions;
    using Battleships.Domain.Ships;
    using Battleships.Domain.Common;

    public class StartPoint
    {
        public StartPoint(Point point, string directionAsText)
        {
            Point = point.NonNull(nameof(point));
            ShipPosition = directionAsText
                    .NonEmpty(nameof(directionAsText))
                    .PatternMatch(RegexPatterns.DirectionPattern, nameof(directionAsText))
                    .ToUpper() == "H" ? ShipPosition.Horizontal : ShipPosition.Vertical;
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
