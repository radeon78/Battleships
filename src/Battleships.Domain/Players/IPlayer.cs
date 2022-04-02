namespace Battleships.Domain.Players
{
    using Battleships.Domain.Common;
    using Battleships.Domain.Grids;
    using Battleships.Domain.PlayRules;
    using System.Threading;

    public interface IPlayer
    {
        string PlayerName { get; }

        void ApplyGameRule(IPlayRule playRule);

        void PlaceShipsOnOceanGrid(CancellationToken cancellationToken);

        Point CallOutPointOnTargetingGrid();

        Result<Answer> AnswerToAttacker(Point attackerPoint);

        void SetDefenderAnswer(Point attackerPoint, Answer answer);

        bool AllShipsSunk();
    }
}
