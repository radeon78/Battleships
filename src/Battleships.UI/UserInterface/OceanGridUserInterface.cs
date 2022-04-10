namespace Battleships.UI.UserInterface
{
    using Battleships.Domain.Extensions;
    using Battleships.Domain.Grids;
    using System;
    using System.Threading;

    public static class OceanGridUserInterface
    {
        public static StartPoint GetPlaceShipStartPoint(string message, CancellationTokenSource tokenSource)
        {
            Console.WriteLine($"\n{message}");
            var column = CommonUserInterface.GetColumn(tokenSource);
            var row = CommonUserInterface.GetRow(tokenSource);
            var direction = GetDirection(tokenSource);

            return new StartPoint(new Point(column, row), direction);
        }

        private static Direction GetDirection(CancellationTokenSource tokenSource)
        {
            Console.WriteLine($"\nType direction (H - {Direction.Horizontal}, V - {Direction.Vertical}): ");
            ConsoleKeyInfo directionKey;
            do
            {
                directionKey = Console.ReadKey();
                if (directionKey.Key.ToString().Equals("q", StringComparison.OrdinalIgnoreCase))
                {
                    tokenSource.Cancel();
                    return Direction.Horizontal;
                }
            }
            while ((int)directionKey.Key is not (72 or 86));

            var direction = (int)directionKey.Key == 72
                ? Direction.Horizontal
                : Direction.Vertical;

            Console.WriteLine();
            return direction;
        }

        public static void PrintOceanGrid(string playerName, OceanGrid oceanGrid)
        {
            Console.WriteLine($"\n{playerName}'s Ocean Grid");
            oceanGrid.PrintGrid((column, row) =>
            {
                var pointStatus = string.Empty;

                oceanGrid.OceanPoints[column, row].FillOut()
                    .IfTrue(() => pointStatus = "X");

                oceanGrid.OceanPoints[column, row].Hit()
                    .IfTrue(() => pointStatus = "-");

                return pointStatus;
            });
        }
    }
}
