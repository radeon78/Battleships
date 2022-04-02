namespace Battleships.Domain.Players
{
    using Battleships.Domain.PlayRules;

    public interface IPlayer
    {
        void ApplyGameRule(IPlayRule playRule);

        void PlaceShipsOnGrid();
    }
}
