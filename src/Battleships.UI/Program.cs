namespace Battleships.UI
{
    using Battleships.Domain;
    using Battleships.Domain.PlayRules;
    using System;
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            var game = new BattleshipsGame(
                new ThreeShipsPlayRule(),
                (message) => Console.WriteLine($"\n{message}"));

            game.StartGame(
                PlayerFactory.CreateHumanPlayer(tokenSource),
                PlayerFactory.CreateComputerPlayer(),
                token);
        }
    }
}
