namespace Battleships.UI
{
    using Battleships.Domain.Players;
    using Battleships.UI.UserInterface;
    using System.Threading;

    public static class PlayerFactory
    {
        public static IPlayer CreateComputerPlayer() 
            => new ComputerPlayer(playerName: "Computer");

        public static IPlayer CreateHumanPlayer(CancellationTokenSource tokenSource)
            => new HumanPlayer(
                playerName: CommonUserInterface.GetHumanPlayerName(tokenSource),
                getPlaceShipStartPoint: (message) => OceanGridUserInterface.GetPlaceShipStartPoint(message, tokenSource),
                printOceanGrid: (playerName, oceanGrid) => OceanGridUserInterface.PrintOceanGrid(playerName, oceanGrid),
                callOutPointOnTargetingGrid: (message) => TargetingGridUserInterface.CallOutPointOnTargetingGrid(message, tokenSource),
                printTargetingGrid: (playerName, targetingGrid) => TargetingGridUserInterface.PrintTargetingGrid(playerName, targetingGrid),
                printErrorMessage: (message) => CommonUserInterface.PrintErrorMessage(message));
    }
}
