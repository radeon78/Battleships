namespace Battleships.Domain.Grids
{
    using Battleships.Domain.Players;
    using Battleships.Domain.Ships;
    using System;

    public class OceanPoint
    {
        private bool _fillOut;
        private bool _hit;
        private Ship _ship;

        public OceanPoint()
        {
            _hit = false;
            _fillOut = false;
            _ship = Ship.CreateEmptyShip();
        }

        public OceanPoint(OceanPoint oceanPoint)
        {
            _fillOut = oceanPoint._fillOut;
            _hit = oceanPoint._hit;
            _ship = new Ship(oceanPoint._ship);
        }

        public bool NotFillOut() => !_fillOut;

        public bool FillOut() => _fillOut;

        public bool Hit() => _hit;

        public Answer TryHit()
        {
            if (NotFillOut())
                return Answer.CreateMissAnswer();

            if (!_hit)
            {
                _ship.Hit();
                _hit = true;
            }

            return _ship.Sunk()
                ? new Answer(_ship.Length, Reply.Sunk)
                : new Answer(_ship.Length, Reply.Hit);
        }

        public void Put(Ship ship)
        {
            _ship = ship;
            _fillOut = true;
        }

        public override bool Equals(object? obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
                return false;

            var otherOceanPoint = (OceanPoint)obj;
            return _fillOut == otherOceanPoint._fillOut &&
                _hit == otherOceanPoint._hit &&
                _ship.Equals(otherOceanPoint._ship);
        }

        public override int GetHashCode() 
            => HashCode.Combine(_fillOut.GetHashCode(), _hit.GetHashCode(), _ship.GetHashCode());
    }
}