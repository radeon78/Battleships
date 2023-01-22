using Battleships.Domain.Grids;

namespace Battleships.Domain.Players;

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