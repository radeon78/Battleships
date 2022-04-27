namespace Battleships.Domain.Grids;

using Battleships.Domain.Extensions;
using Battleships.Domain.Players;
using Battleships.Domain.Ships;

public class OceanPoint
{
    private bool _fillOut;
    private bool _hit;
    private Ship _ship;

    internal OceanPoint()
    {
        _hit = false;
        _fillOut = false;
        _ship = Ship.CreateEmptyShip();
    }

    public bool NotFillOut() => !_fillOut;

    public bool FillOut() => _fillOut;

    public bool Hit() => _hit;

    internal Answer TryHit()
    {
        if (NotFillOut())
            return Answer.CreateMissAnswer();

        _hit.WhenFalse(() =>
        {
            _ship.Hit();
            _hit = true;
        });
            
        return _ship.Sunk()
            ? new Answer(_ship.Length, Reply.Sunk)
            : new Answer(_ship.Length, Reply.Hit);
    }

    internal void Put(Ship ship)
    {
        _ship = ship;
        _fillOut = true;
    }
}