namespace Battleships.Domain.Grids
{
    public static class Grid
    {
        public static int Size => 10;

        internal static bool PointIsOutOfGrid(this Point point)
        {
            return point.Column > Size - 1 ||
                   point.Column < 0 ||
                   point.Row > Size - 1 ||
                   point.Row < 0;
        }
    }
}
