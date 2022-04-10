namespace Battleships.Domain.Grids
{
    using Battleships.Domain.Players;
    using Battleships.Domain.Ships;
    using System;

    public class OceanPoint
    {
        private bool _isFillOut;
        private bool _isHit;
        private Ship _ship;

        public OceanPoint()
        {
            _isHit = false;
            _isFillOut = false;
            _ship = Ship.CreateEmptyShip();
        }

        public OceanPoint(OceanPoint oceanPoint)
        {
            _isFillOut = oceanPoint._isFillOut;
            _isHit = oceanPoint._isHit;
            _ship = new Ship(oceanPoint._ship);
        }

        public bool NotFillOut() => !_isFillOut;

        public bool FillOut() => _isFillOut;

        public bool Hit() => _isHit;

        public Answer TryHit()
        {
            if (NotFillOut())
                return Answer.CreateMissAnswer();

            if (!_isHit)
            {
                _ship.Hit();
                _isHit = true;
            }

            return _ship.IsSunk()
                ? new Answer(_ship.Length, Reply.Sunk)
                : new Answer(_ship.Length, Reply.Hit);
        }

        public void Put(Ship ship)
        {
            _ship = ship;
            _isFillOut = true;
        }

        public override bool Equals(object? obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
                return false;

            var otherOceanPoint = (OceanPoint)obj;
            return _isFillOut == otherOceanPoint._isFillOut &&
                _isHit == otherOceanPoint._isHit &&
                _ship.Equals(otherOceanPoint._ship);
        }

        public override int GetHashCode() 
            => HashCode.Combine(_isFillOut.GetHashCode(), _isHit.GetHashCode(), _ship.GetHashCode());
    }
}