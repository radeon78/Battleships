﻿namespace Battleships.UI
{
    using Battleships.Domain;
    using System;
    using System.Threading;

    internal static class Program
    {
        internal static void Main()
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            var game = new BattleshipsGame(
                gameRule: new ThreeShipsGameRule(),
                printMessage: (message) => Console.WriteLine($"\n{message}"));

            game.Start(
                firstPlayer: PlayerFactory.CreateHumanPlayer(tokenSource),
                secondPlayer: PlayerFactory.CreateComputerPlayer("Computer"),
                cancellationToken: token);
        }
    }
}
