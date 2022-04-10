namespace Battleships.Domain.Players
{
    using Battleships.Domain.PlayRules;
    using System.Threading;

    public interface IPlayer : IAttackerPlayer, IDefenderPlayer
    {
        void ApplyGameRule(IPlayRule playRule);

        void PlaceShipsOnOceanGrid(CancellationToken cancellationToken);
    }
}
