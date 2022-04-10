namespace Battleships.UI
{
    using Battleships.Domain.Players;
    using Battleships.UI.UserInterface;
    using System.Threading;

    public static class PlayerFactory
    {
        public static IPlayer CreateComputerPlayer() => new ComputerPlayer("Computer");

        public static IPlayer CreateHumanPlayer(CancellationTokenSource tokenSource)
            => new HumanPlayer(
                CommonUserInterface.GetHumanPlayerName(tokenSource),
                (message) => OceanGridUserInterface.GetPlaceShipStartPoint(message, tokenSource),
                (playerName, oceanGrid) => OceanGridUserInterface.PrintOceanGrid(playerName, oceanGrid),
                (message) => TargetingGridUserInterface.CallOutPointOnTargetingGrid(message, tokenSource),
                (playerName, targetingGrid) => TargetingGridUserInterface.PrintTargetingGrid(playerName, targetingGrid),
                (message) => CommonUserInterface.PrintErrorMessage(message));
    }
}
