namespace Battleships.Domain.Players
{
    using Battleships.Domain.Grids;
    using Battleships.Domain.PlayRules;
    using System.Threading;

    public interface IPlayer
    {
        string PlayerName { get; }

        void ApplyGameRule(IPlayRule playRule);

        void PlaceShipsOnOceanGrid(CancellationToken cancellationToken);

        Point CallOutPointOnTargetingGrid();

        Answer AnswerToAttacker(Point attackerPoint);

        void SetDefenderAnswer(Point attackerPoint, Answer answer);

        bool AllShipsSunk();

        void PrintOceanGrid();
    }
}
