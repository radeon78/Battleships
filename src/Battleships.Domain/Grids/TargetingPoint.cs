namespace Battleships.Domain.Grids
{
    using Battleships.Domain.Extensions;
    using Battleships.Domain.Players;
    using System;

    public class TargetingPoint
    {
        private bool _isHit;
        private bool _isSunk;
        private bool _isCalledOut;
        private int _shipLength;

        public TargetingPoint()
        {
            _isHit = false;
            _isCalledOut = false;
            _shipLength = 0;
        }

        public TargetingPoint(TargetingPoint targetingPoint)
        {
            _isHit = targetingPoint._isHit;
            _isCalledOut = targetingPoint._isCalledOut;
            _shipLength = targetingPoint._shipLength;
            _isSunk = targetingPoint._isSunk;
        }

        public bool Hit() => _isHit;

        public string DisplayShipLength() => _shipLength.ToString();

        public bool Miss() => _isCalledOut && !_isHit;

        public void SetAnswer(Answer answer)
        {
            _isCalledOut = true;

            if (answer.Reply == Reply.Miss) return;

            _isHit = true;
            _shipLength = answer.ShipLength;

            (answer.Reply == Reply.Sunk)
                .IfTrue(() => _isSunk = true);
        }

        public override bool Equals(object? obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
                return false;

            var otherTargetingPoint = (TargetingPoint)obj;
            return _isHit == otherTargetingPoint._isHit &&
                _isSunk == otherTargetingPoint._isSunk &&
                _isCalledOut == otherTargetingPoint._isCalledOut &&
                _shipLength == otherTargetingPoint._shipLength;
        }

        public override int GetHashCode()
            => HashCode.Combine(
                _isHit.GetHashCode(),
                _isSunk.GetHashCode(),
                _isCalledOut.GetHashCode(),
                _shipLength.GetHashCode());
    }
}