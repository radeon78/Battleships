namespace Battleships.UI.UserInterface
{
    using Battleships.Domain.Grids;
    using System;

    public static class OceanGrid
    {
        public static StartPoint GetPlaceShipStartPoint(string message)
        {
            Console.WriteLine($"\n{message}");
            var column = Common.GetColumn();
            var row = Common.GetRow();
            var direction = GetDirection();

            return new StartPoint(new Point(column, row), direction);
        }

        private static Direction GetDirection()
        {
            Console.WriteLine($"\nType direction ({(int)Direction.Horizontal} - {Direction.Horizontal}, {(int)Direction.Vertical} - {Direction.Vertical}): ");
            ConsoleKeyInfo directionKey;
            do
            {
                directionKey = Console.ReadKey();
            }
            while ((int)directionKey.Key is < 48 or > 49);
            var direction = (Direction)((int)directionKey.Key - 48);

            Console.WriteLine();
            return direction;
        }
    }
}
