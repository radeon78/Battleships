namespace Battleships.UI
{
    using Battleships.Domain;
    using System;
    using System.Threading;

    static class Program
    {
        static void Main()
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            var game = new BattleshipsGame(
                gameRule: new ThreeShipsGameRule(),
                printMessage: (message) => Console.WriteLine($"\n{message}"));

            game.Start(
                firstPlayer: PlayerFactory.CreateHumanPlayer(tokenSource),
                secondPlayer: PlayerFactory.CreateComputerPlayer(),
                cancellationToken: token);
        }
    }
}
