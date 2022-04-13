namespace Battleships.Domain.Players
{
    using Battleships.Domain;
    using System.Threading;

    public interface IPlayer : IAttackerPlayer, IDefenderPlayer
    {
        void ApplyGameRule(IGameRule gameRule);

        void PlaceShipsOnOceanGrid(CancellationToken cancellationToken);
    }
}
