using System.Text.RegularExpressions;
using Battleships.Domain.Common;
using Battleships.Domain.Grids;
using Battleships.UI.Resources;

namespace Battleships.UI.UserInterface;

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

            if (targetingGrid[column, row].Miss())
                pointStatus = shipIsMiss;

            if (targetingGrid[column, row].Hit())
                pointStatus = targetingGrid[column, row].DisplayShipLength();

            return pointStatus;
        });
    }
}