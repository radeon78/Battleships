namespace Battleships.UI
{
    using Battleships.Domain;
    using Battleships.Domain.Players;
    using Battleships.Domain.PlayRules;
    using Battleships.UI.UserInterface;
    using System;
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {
            var source = new CancellationTokenSource();
            var token = source.Token;

            var game = new BattleshipsGame(
                new ThreeShipsPlayRule(),
                (message) => Console.WriteLine($"\n{message}"));

            game.StartGame(
                new HumanPlayer(
                    "Zbyszko z Bogdanca",
                    (message) => OceanGridUserInterface.GetPlaceShipStartPoint(message),
                    (playerName, oceanGrid) => OceanGridUserInterface.PrintOceanGrid(playerName, oceanGrid),
                    (message) => TargetingGridUserInterface.CallOutPointOnTargetingGrid(message),
                    (playerName, targetingGrid) => TargetingGridUserInterface.PrintTargetingGrid(playerName, targetingGrid),
                    (message) => CommonUserInterface.PrintErrorMessage(message)),
                new ComputerPlayer("Computer"),
                token);
        }
    }
}
