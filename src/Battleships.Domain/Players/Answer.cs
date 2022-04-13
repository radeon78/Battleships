namespace Battleships.Domain.Players
{
    public class Answer
    {
        public Answer(int shipLength, Reply reply)
        {
            ShipLength = shipLength;
            Reply = reply;
        }

        public Reply Reply { get; }

        public int ShipLength { get; }

        public static Answer CreateMissAnswer()
            => new(0, Reply.Miss);

        public override string ToString()
            => Reply == Reply.Miss
                ? Reply.ToString()
                : $"{Reply} {ShipLength} length ship";
    }
}
