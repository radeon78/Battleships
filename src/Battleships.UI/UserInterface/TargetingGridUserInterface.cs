namespace Battleships.UI.UserInterface
{
    using Battleships.UI.Resources;
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
            Console.WriteLine(Environment.NewLine + message);
            var pointAsText = CommonUserInterface.GetInputDataFromUser(
                displayMessageToUser: Resource.TypePoint,
                inputValid: input => Regex.Match(input, RegexPatterns.PointPattern).Success,
                tokenSource: tokenSource);

            return tokenSource.IsCancellationRequested
                ? Point.CreateEmptyPoint()
                : new Point(pointAsText);
        }

        public static void PrintTargetingGrid(string playerName, TargetingGrid targetingGrid)
        {
            const string missChar = "-";

            Console.WriteLine(Resource.PlayerTargetingGrid, Environment.NewLine, playerName);
            CommonUserInterface.PrintGrid((column, row) =>
            {
                var pointStatus = string.Empty;

                targetingGrid.TargetingPoints[column, row].Miss()
                    .IfTrue(() => pointStatus = missChar);

                targetingGrid.TargetingPoints[column, row].Hit()
                    .IfTrue(() => pointStatus = targetingGrid.TargetingPoints[column, row].DisplayShipLength());

                return pointStatus;
            });
        }
    }
}
