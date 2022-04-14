namespace Battleships.Domain.Grids
{
    public class StartPoint
    {
        public StartPoint(Point point, Direction direction)
        {
            Point = point;
            Direction = direction;
        }

        internal Point Point { get; }

        internal Direction Direction { get; }
    }
}
