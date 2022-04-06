namespace Battleships.UI.UserInterface
{
    using Battleships.Domain.Extensions;
    using Battleships.Domain.Grids;
    using ConsoleTables;
    using System;

    public static class TargetingGridUserInterface
    {
        public static Point CallOutPointOnTargetingGrid(string message)
        {
            Console.WriteLine($"\n{message}");
            var column = CommonUserInterface.GetColumn();
            var row = CommonUserInterface.GetRow();
            
            return new Point(column, row);
        }

        public static void PrintTargetingGrid(TargetingGrid targetingGrid)
        {
            Console.WriteLine($"\nThe Targeting Grid");

            var columns = new string[targetingGrid.Size + 1];

            columns[0] = string.Empty;
            for (var i = 0; i < targetingGrid.Size; i++)
                columns[i + 1] = i.ToDisplayColumnAsString();

            var table = new ConsoleTable(columns);

            for (var i = 0; i < targetingGrid.Size; i++)
            {
                var row = new string[targetingGrid.Size + 1];
                row[0] = i.ToDisplayRow();
                for (var j = 0; j < targetingGrid.Size; j++)
                {
                    row[j + 1] = string.Empty;

                    targetingGrid.TargetingPoints[j, i].Miss()
                        .IfTrue(() => row[j + 1] = "-");

                    targetingGrid.TargetingPoints[j, i].Hit()
                        .IfTrue(() => row[j + 1] = "X");
                }

                table.AddRow(row);
            }

            table.Write();
        }
    }
}
