using Battleships.Domain.Players;
using Battleships.UI.UserInterface;

namespace Battleships.UI;

public static class PlayerFactory
{
    public static Player CreateComputerPlayer(string computerName) 
        => new ComputerPlayer(playerName: computerName);

    public static Player CreateHumanPlayer(CancellationTokenSource tokenSource)
        => new HumanPlayer(
            playerName: CommonUserInterface.GetHumanPlayerName(tokenSource),
            getPlaceShipStartPoint: message => OceanGridUserInterface.GetPlaceShipStartPoint(message, tokenSource),
            printOceanGrid: (playerName, oceanGrid) => OceanGridUserInterface.PrintOceanGrid(playerName, oceanGrid),
            callOutPointOnTargetingGrid: message => TargetingGridUserInterface.CallOutPointOnTargetingGrid(message, tokenSource),
            printTargetingGrid: (playerName, targetingGrid) => TargetingGridUserInterface.PrintTargetingGrid(playerName, targetingGrid),
            printErrorMessage: message => CommonUserInterface.PrintErrorMessage(message));
}