namespace Battleships.UI.UserInterface
{
    using Battleships.Domain.Common;
    using System.Text.RegularExpressions;
    using Battleships.Domain.Extensions;
    using Battleships.Domain.Grids;
    using System;
    using System.Threading;

    public static class TargetingGridUserInterface
    {
        public static Point CallOutPointOnTargetingGrid(string message, CancellationTokenSource tokenSource)
        {
            Console.WriteLine($"\n{message}");
            var pointAsText = CommonUserInterface.GetInputDataFromUser(
                displayMessageToUser: "\nType start point (e.g. A1 where A is a column and 1 is a row) : ",
                inputValid: input => Regex.Match(input, RegexPatterns.PointPattern).Success,
                tokenSource: tokenSource);

            return tokenSource.IsCancellationRequested
                ? Point.CreateEmptyPoint()
                : new Point(pointAsText);
        }

        public static void PrintTargetingGrid(string playerName, TargetingGrid targetingGrid)
        {
            Console.WriteLine($"\n{playerName}'s Targeting Grid");
            CommonUserInterface.PrintGrid((column, row) =>
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
