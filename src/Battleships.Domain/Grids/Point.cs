namespace Battleships.Domain.Grids
{
    using Battleships.Domain.Extensions;

    public class Point
    {
        public Point(int column, int row)
        {
            Column = column;
            Row = row;
        }

        public int Column { get; }

        public int Row { get; }

        public override string ToString()
            => $"{Column.ToDisplayColumn()}{Row.ToDisplayRow()}";
    }
}
