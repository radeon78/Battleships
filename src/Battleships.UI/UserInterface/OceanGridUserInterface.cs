﻿namespace Battleships.UI.UserInterface
{
    using Battleships.UI.Resources;
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
            const string shipChar = "O";
            const string shipHitChar = "X";

            Console.WriteLine(Resource.PlayerOceanGrid, Environment.NewLine, playerName);
            CommonUserInterface.PrintGrid((column, row) =>
            {
                var pointStatus = string.Empty;

                oceanGrid.OceanPoints[column, row].FillOut()
                    .IfTrue(() => pointStatus = shipChar);

                oceanGrid.OceanPoints[column, row].Hit()
                    .IfTrue(() => pointStatus = shipHitChar);

                return pointStatus;
            });
        }
    }
}
