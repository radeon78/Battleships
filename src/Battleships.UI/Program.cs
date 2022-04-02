namespace Battleships.UI
{
    using Battleships.Domain;
    using Battleships.Domain.Players;
    using Battleships.Domain.PlayRules;
    using Battleships.UI.UserInterfaces;
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
                    (message) => PlaceShip.GetPlaceShipStartPoint(message),
                    (message) => PlaceShip.PlaceShipNotAllowed(message)),
                new ComputerPlayer("Computer"),
                token);
        }
    }
}
