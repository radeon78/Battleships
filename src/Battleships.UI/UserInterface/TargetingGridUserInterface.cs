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
            const string shipIsMiss = "-";

            Console.WriteLine(Resource.PlayerTargetingGrid, Environment.NewLine, playerName);
            CommonUserInterface.PrintGrid((column, row) =>
            {
                var pointStatus = string.Empty;

                targetingGrid[column, row].Miss()
                    .IfTrue(() => pointStatus = shipIsMiss);

                targetingGrid[column, row].Hit()
                    .IfTrue(() => pointStatus = targetingGrid[column, row].DisplayShipLength());

                return pointStatus;
            });
        }
    }
}
