namespace Battleships.Domain.Ships
{
    using Battleships.Domain;
    using System;

    public class Ship
    {
        private const int _minLength = 1;
        private const int _maxLength = 5;

        private Ship() => Length = 0;

        public Ship(int length)
        {
            if (length is < _minLength or > _maxLength)
            {
                throw new ArgumentException(
                    string.Format(Resource.ErrorFieldMustBeBetween, _minLength, _maxLength), nameof(length));
            }

            Length = length;
        }

        public int Length { get; }

        public override string ToString() => $"{Length} length ship";

        public static Ship CreateEmptyShip() => new();

        public static Ship CreateBattleship() => new(5);

        public static Ship CreateDestroyer() => new(4);
    }
}
