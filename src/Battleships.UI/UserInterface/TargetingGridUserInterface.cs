namespace Battleships.UI.UserInterface
{
    using Battleships.Domain.Extensions;
    using Battleships.Domain.Grids;
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

        public static void PrintTargetingGrid(string playerName, TargetingGrid targetingGrid)
        {
            Console.WriteLine($"\n{playerName}'s Targeting Grid");
            targetingGrid.PrintGrid((column, row) =>
            {
                var pointStatus = string.Empty;

                targetingGrid.TargetingPoints[column, row].Miss()
                    .IfTrue(() => pointStatus = "-");

                targetingGrid.TargetingPoints[column, row].Hit()
                    .IfTrue(() => pointStatus = targetingGrid.TargetingPoints[column, row].DisplayShipLength());

                return pointStatus;
            });
        }
    }
}
