namespace Battleships.Domain.Grids
{
    using Battleships.Domain.Ships;

    public class OceanPoint
    {
        private bool _isFillOut;
        private Ship _ship;

        public OceanPoint()
        {
            _isFillOut = false;
            _ship = Ship.CreateEmptyShip();
        }

        public bool IsNotFillOut() => !_isFillOut;

        public bool IsFillOut() => _isFillOut;

        public void Put(Ship ship)
        {
            _ship = ship;
            _isFillOut = true;
        }
    }
}