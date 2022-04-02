namespace Battleships.Domain.Grids
{
    using Battleships.Domain.Players;

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

        public void SetAnswer(Answer answer)
        {
            _isCalledOut = true;

            if (answer.Reply == Reply.Miss)
            {
                return;
            }

            _isHit = true;
            _shipLength = answer.ShipLength;
            if (answer.Reply == Reply.Sunk) _isSunk = true;
        }
    }
}