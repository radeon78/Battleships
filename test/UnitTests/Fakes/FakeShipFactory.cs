namespace UnitTests.Fakes;

using Battleships.Domain.Ships;

public static class FakeShipFactory
{
    public static Ship CreateBattleship() => new(5);

    public static Ship CreateDestroyer() => new(4);

    public static Ship CreateSubmarine() => new(2);
}