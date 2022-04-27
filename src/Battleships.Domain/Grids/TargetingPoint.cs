namespace Battleships.Domain.Grids;

using Battleships.Domain.Players;

public class TargetingPoint
{
    private bool _hit;
    private bool _calledOut;
    private int _shipLength;

    internal TargetingPoint()
    {
        _hit = false;
        _calledOut = false;
        _shipLength = 0;
    }

    public bool Hit() => _hit;

    public string DisplayShipLength() => _shipLength.ToString();

    public bool Miss() => _calledOut && !_hit;

    internal bool CalledOut() => _calledOut;

    internal void SetAnswer(Answer answer)
    {
        _calledOut = true;

        if (answer.Reply == Reply.Miss) return;

        _hit = true;
        _shipLength = answer.ShipLength;
    }
}