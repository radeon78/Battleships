namespace Battleships.Domain.Grids
{
    using Battleships.Domain.Players;
    using Battleships.Domain.Ships;

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

        public bool IsNotFillOut() => !_isFillOut;

        public Answer TryHit()
        {
            if (IsNotFillOut())
            {
                return Answer.CreateMissAnswer();
            }

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
    }
}