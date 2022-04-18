namespace Battleships.UI.UserInterface
{
    using Battleships.Domain.Common;
    using System.Text.RegularExpressions;
    using Battleships.Domain.Extensions;
    using Battleships.Domain.Grids;
    using System;
    using System.Threading;

    public static class OceanGridUserInterface
    {
        public static StartPoint GetPlaceShipStartPoint(string message, CancellationTokenSource tokenSource)
        {
            Console.WriteLine($"\n{message}");

            var pointAsText = CommonUserInterface.GetInputDataFromUser(
                displayMessageToUser: "\nType start point (e.g. A1 where A is a column and 1 is a row) : ",
                inputValid: input => Regex.Match(input, RegexPatterns.PointPattern).Success,
                tokenSource: tokenSource);

            if (tokenSource.IsCancellationRequested)
                return StartPoint.CreateEmptyStartPoint();

            var directionAsText = CommonUserInterface.GetInputDataFromUser(
                displayMessageToUser: $"\nType a direction from a start point a ship will be placed. Choose a H or V (H - {Direction.Horizontal}, V - {Direction.Vertical}): ",
                inputValid: input => Regex.Match(input, RegexPatterns.DirectionPattern).Success,
                tokenSource: tokenSource);

            return tokenSource.IsCancellationRequested
                ? StartPoint.CreateEmptyStartPoint()
                : new StartPoint(new Point(pointAsText), directionAsText);
        }

        public static void PrintOceanGrid(string playerName, OceanGrid oceanGrid)
        {
            Console.WriteLine($"\n{playerName}'s Ocean Grid");
            CommonUserInterface.PrintGrid((column, row) =>
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
