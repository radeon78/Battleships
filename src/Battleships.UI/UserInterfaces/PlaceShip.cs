namespace Battleships.UI.UserInterfaces
{
    using Battleships.Domain.Grids;
    using System;

    public static class PlaceShip
    {
        public static StartPoint GetPlaceShipStartPoint(string message)
        {
            Console.WriteLine($"\n{message}");
            var column = GetColumn();
            var row = GetRow();
            var direction = GetDirection();

            return new StartPoint(new Point(column, row), direction);
        }

        public static void PlaceShipNotAllowed(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{message}");
            Console.ResetColor();
        }

        private static int GetColumn()
        {
            Console.WriteLine($"\nType column (A-J): ");
            ConsoleKeyInfo columnKey;
            do
            {
                columnKey = Console.ReadKey();
            }
            while ((int)columnKey.Key < 65 || (int)columnKey.Key > 74);
            var column = (int)columnKey.Key - 65;

            return column;
        }

        private static int GetRow()
        {
            Console.WriteLine($"\nType row (1-10): ");
            string? input;

            do
            {
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input) && input.Length <= 2)
                {
                    if (int.TryParse(input, out var result) && result >= 1 && result <= 10)
                        return result - 1;
                }
            }
            while (true);
        }

        private static Direction GetDirection()
        {
            Console.WriteLine($"\nType direction ({(int)Direction.Horizontal} - {Direction.Horizontal}, {(int)Direction.Vertical} - {Direction.Vertical}): ");
            ConsoleKeyInfo directionKey;
            do
            {
                directionKey = Console.ReadKey();
            }
            while ((int)directionKey.Key < 48 || (int)directionKey.Key > 49);
            var direction = (Direction)((int)directionKey.Key - 48);

            Console.WriteLine();
            return direction;
        }
    }
}
