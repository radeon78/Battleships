namespace Battleships.Domain.Ships
{
    using Battleships.Domain.Extensions;
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
            Length = length.InRange(
                _minLength,
                _maxLength,
                string.Format(Resource.ErrorFieldMustBeBetween, _minLength, _maxLength));
        }

        internal int Length { get; } = 0;

        internal void Hit()
        {
            (_hits < Length)
                .IfTrue(() => _hits++)
                .IfFalse(() => throw new ArgumentOutOfRangeException(string.Format(Resource.ErrorToManyHitsOnShip, this)));
        }

        internal bool Sunk() => _hits == Length;

        public override string ToString() => $"{Length} length ship";

        internal static Ship CreateEmptyShip() => new();
    }
}
