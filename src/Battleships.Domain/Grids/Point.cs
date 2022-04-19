namespace Battleships.Domain.Grids
{
    using Battleships.Domain.Common;
    using System.Text.RegularExpressions;
    using System;
    using Battleships.Domain.Extensions;

    public class Point
    {
        public Point(string pointAsText)
        {
            string.IsNullOrEmpty(pointAsText)
                .IfTrue(() => throw new ArgumentNullException(nameof(pointAsText)));

            Regex.Match(pointAsText, RegexPatterns.PointPattern).Success
                .IfFalse(() => throw new ArgumentException(pointAsText, nameof(pointAsText)));
            
            Column = pointAsText[0].ToNumberColumn();
            Row = int.Parse(pointAsText.Remove(0, 1)) - 1;
        }

        public Point(int column, int row)
        {
            Column = column;
            Row = row;
        }

        internal int Column { get; }

        internal int Row { get; }

        public override string ToString()
            => $"{Column.ToDisplayColumn()}{Row.ToDisplayRow()}";

        public static Point CreateEmptyPoint()
            => new(0, 0);
    }
}
