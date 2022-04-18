namespace Battleships.Domain.Grids
{
    using Battleships.Domain.Extensions;
    using Battleships.Domain.Players;
    using System;

    public class TargetingPoint
    {
        private bool _hit;
        private bool _sunk;
        private bool _calledOut;
        private int _shipLength;

        internal TargetingPoint()
        {
            _hit = false;
            _calledOut = false;
            _shipLength = 0;
        }

        internal TargetingPoint(TargetingPoint targetingPoint)
        {
            _hit = targetingPoint._hit;
            _calledOut = targetingPoint._calledOut;
            _shipLength = targetingPoint._shipLength;
            _sunk = targetingPoint._sunk;
        }

        public bool Hit() => _hit;

        public string DisplayShipLength() => _shipLength.ToString();

        public bool Miss() => _calledOut && !_hit;

        internal void SetAnswer(Answer answer)
        {
            _calledOut = true;

            if (answer.Reply == Reply.Miss) return;

            _hit = true;
            _shipLength = answer.ShipLength;

            (answer.Reply == Reply.Sunk)
                .IfTrue(() => _sunk = true);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var otherTargetingPoint = (TargetingPoint)obj;
            return _hit == otherTargetingPoint._hit &&
                _sunk == otherTargetingPoint._sunk &&
                _calledOut == otherTargetingPoint._calledOut &&
                _shipLength == otherTargetingPoint._shipLength;
        }

        public override int GetHashCode()
            => HashCode.Combine(
                _hit.GetHashCode(),
                _sunk.GetHashCode(),
                _calledOut.GetHashCode(),
                _shipLength.GetHashCode());
    }
}