namespace Battleships.Domain.Grids
{
    public class StartPoint
    {
        public StartPoint(Point point, Direction direction)
        {
            Point = point;
            Direction = direction;
        }

        public Point Point { get; }

        public Direction Direction { get; }
    }
}
