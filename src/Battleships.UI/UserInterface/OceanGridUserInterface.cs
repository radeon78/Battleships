using System.Text.RegularExpressions;
using Battleships.Domain.Common;
using Battleships.Domain.Grids;
using Battleships.UI.Resources;

namespace Battleships.UI.UserInterface;

public static class OceanGridUserInterface
{
    public static StartPoint GetPlaceShipStartPoint(string message, CancellationTokenSource tokenSource)
    {
        Console.WriteLine(Environment.NewLine + message);

        var pointAsText = CommonUserInterface.GetInputDataFromUser(
            displayMessageToUser: Resource.TypeStartPoint,
            inputValid: input => Regex.Match(input, RegexPatterns.PointPattern).Success,
            tokenSource: tokenSource);

        if (tokenSource.IsCancellationRequested)
            return StartPoint.CreateEmptyStartPoint();

        var directionAsText = CommonUserInterface.GetInputDataFromUser(
            displayMessageToUser: Resource.TypeDirection,
            inputValid: input => Regex.Match(input, RegexPatterns.DirectionPattern).Success,
            tokenSource: tokenSource);

        return tokenSource.IsCancellationRequested
            ? StartPoint.CreateEmptyStartPoint()
            : new StartPoint(new Point(pointAsText), directionAsText);
    }

    public static void PrintOceanGrid(string playerName, OceanGrid oceanGrid)
    {
        const string ship = "O";
        const string shipIsHit = "X";

        Console.WriteLine(Resource.PlayerOceanGrid, Environment.NewLine, playerName);
        CommonUserInterface.PrintGrid((column, row) =>
        {
            var pointStatus = string.Empty;

            if (oceanGrid[column, row].FillOut())
                pointStatus = ship;

            if (oceanGrid[column, row].Hit())
                pointStatus = shipIsHit;

            return pointStatus;
        });
    }
}