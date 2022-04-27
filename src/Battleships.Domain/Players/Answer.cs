namespace Battleships.Domain.Players;

public class Answer
{
    internal Answer(int shipLength, Reply reply)
    {
        ShipLength = shipLength;
        Reply = reply;
    }

    internal Reply Reply { get; }

    internal int ShipLength { get; }

    internal static Answer CreateMissAnswer()
        => new(0, Reply.Miss);

    public override string ToString()
        => Reply == Reply.Miss
            ? Reply.ToString()
            : $"{Reply} {ShipLength} length ship";
}