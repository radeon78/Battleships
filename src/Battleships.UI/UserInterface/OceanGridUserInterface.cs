namespace Battleships.UI.UserInterface
{
    using Battleships.Domain.Extensions;
    using Battleships.Domain.Grids;
    using ConsoleTables;
    using System;

    public static class OceanGridUserInterface
    {
        public static StartPoint GetPlaceShipStartPoint(string message)
        {
            Console.WriteLine($"\n{message}");
            var column = CommonUserInterface.GetColumn();
            var row = CommonUserInterface.GetRow();
            var direction = GetDirection();

            return new StartPoint(new Point(column, row), direction);
        }

        private static Direction GetDirection()
        {
            Console.WriteLine($"\nType direction (H - {Direction.Horizontal}, V - {Direction.Vertical}): ");
            ConsoleKeyInfo directionKey;
            do
            {
                directionKey = Console.ReadKey();
            }
            while ((int)directionKey.Key is not (72 or 86));

            var direction = (int)directionKey.Key == 72
                ? Direction.Horizontal
                : Direction.Vertical;

            Console.WriteLine();
            return direction;
        }

        public static void PrintOceanGrid(OceanGrid oceanGrid)
        {
            Console.WriteLine($"\nThe Ocean Grid");

            var columns = new string[oceanGrid.Size + 1];

            columns[0] = string.Empty;
            for (var i = 0; i < oceanGrid.Size; i++)
                columns[i + 1] = i.ToDisplayColumnAsString();

            var table = new ConsoleTable(columns);

            for (var i = 0; i < oceanGrid.Size; i++)
            {
                var row = new string[oceanGrid.Size + 1];
                row[0] = i.ToDisplayRow();
                for (var j = 0; j < oceanGrid.Size; j++)
                {
                    row[j + 1] = string.Empty;

                    oceanGrid.OceanPoints[j, i].FillOut()
                        .IfTrue(() => row[j + 1] = "X");

                    oceanGrid.OceanPoints[j, i].Hit()
                        .IfTrue(() => row[j + 1] = "-");
                }

                table.AddRow(row);
            }

            table.Write();
        }
    }
}
