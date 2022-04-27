namespace Battleships.Domain.Players;

using Battleships.Domain.Grids;
using Battleships.Domain;
using System.Threading;

public interface IPlayer
{
    string PlayerName { get; }

    void ApplyGameRule(IGameRule gameRule);

    void PlaceShipsOnOceanGrid(CancellationToken cancellationToken);

    Point CallOutPointOnTargetingGrid();

    void SetDefenderAnswer(Point attackerPoint, Answer answer);

    void PrintTargetingGrind();

    Answer AnswerToAttacker(Point attackerPoint);

    bool AllShipsSunk();

    void PrintOceanGrid();
}