namespace Battleships.Domain.Ships
{
    using Battleships.Domain.Resources;
    using System;

    public class Ship
    {
        private const int _minLength = 1;
        private const int _maxLength = 5;
        private int _hits = 0;

        private Ship() { }

        public Ship(int length)
        {
            if (length is < _minLength or > _maxLength)
            {
                throw new ArgumentException(
                    string.Format(Resource.ErrorFieldMustBeBetween, _minLength, _maxLength), nameof(length));
            }

            Length = length;
        }

        internal Ship(Ship ship)
        {
            _hits = ship._hits;
            Length = ship.Length;
        }

        internal int Length { get; } = 0;

        internal void Hit()
        {
            if (_hits < Length) _hits++;
            else throw new ArgumentOutOfRangeException(string.Format(Resource.ErrorToManyHitsOnShip, this));
        }

        internal bool Sunk() => _hits == Length;

        public override string ToString() => $"{Length} length ship";

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var otherShip = (Ship)obj;
            return _hits == otherShip._hits && Length == otherShip.Length;
        }

        public override int GetHashCode() => HashCode.Combine(_hits, Length);

        internal static Ship CreateEmptyShip() => new();
    }
}
