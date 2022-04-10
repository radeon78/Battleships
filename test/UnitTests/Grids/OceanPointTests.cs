namespace UnitTests.Grids
{
    using Battleships.Domain.Grids;
    using Battleships.Domain.Players;
    using Battleships.Domain.Ships;
    using FluentAssertions;
    using Xunit;

    public class OceanPointTests
    {
        [Fact]
        public void ShouldReturnMiss()
        {
            // arrange
            var oceanPoint = new OceanPoint();

            // act
            var answer1 = oceanPoint.TryHit();
            var answer2 = oceanPoint.TryHit();

            // assert
            oceanPoint.FillOut().Should().BeFalse();
            oceanPoint.NotFillOut().Should().BeTrue();
            oceanPoint.Hit().Should().BeFalse();

            answer1.Should().NotBeNull();
            answer1.Reply.Should().Be(Reply.Miss);
            answer1.ShipLength.Should().Be(0);

            answer2.Should().NotBeNull();
            answer2.Reply.Should().Be(Reply.Miss);
            answer2.ShipLength.Should().Be(0);
        }

        [Fact]
        public void ShouldReturnHit()
        {
            // arrange
            var oceanPoint = new OceanPoint();
            oceanPoint.Put(new Ship(3));

            // act
            var answer = oceanPoint.TryHit();

            // assert
            oceanPoint.FillOut().Should().BeTrue();
            oceanPoint.NotFillOut().Should().BeFalse();
            oceanPoint.Hit().Should().BeTrue();

            answer.Should().NotBeNull();
            answer.Reply.Should().Be(Reply.Hit);
            answer.ShipLength.Should().Be(3);
        }

        [Fact]
        public void ShouldReturnHitForTheSamePoint()
        {
            // arrange
            var oceanPoint = new OceanPoint();
            oceanPoint.Put(new Ship(3));

            // act
            var answer1 = oceanPoint.TryHit();
            var answer2 = oceanPoint.TryHit();
            var answer3 = oceanPoint.TryHit();
            var answer4 = oceanPoint.TryHit();

            // assert
            answer1.Should().NotBeNull();
            answer1.Reply.Should().Be(Reply.Hit);
            answer1.ShipLength.Should().Be(3);

            answer2.Should().NotBeNull();
            answer2.Reply.Should().Be(Reply.Hit);
            answer2.ShipLength.Should().Be(3);

            answer3.Should().NotBeNull();
            answer3.Reply.Should().Be(Reply.Hit);
            answer3.ShipLength.Should().Be(3);

            answer4.Should().NotBeNull();
            answer4.Reply.Should().Be(Reply.Hit);
            answer4.ShipLength.Should().Be(3);
        }

        [Fact]
        public void ShouldReturnHitForTheSamePointAndThenSunkForNextPoint()
        {
            // arrange
            var ship = new Ship(3);
            var oceanPoint1 = new OceanPoint();
            var oceanPoint2 = new OceanPoint();
            var oceanPoint3 = new OceanPoint();

            oceanPoint1.Put(ship);
            oceanPoint2.Put(ship);
            oceanPoint3.Put(ship);

            // act
            var answer1 = oceanPoint1.TryHit();
            var answer2 = oceanPoint2.TryHit();
            var answer21 = oceanPoint2.TryHit();
            var answer22 = oceanPoint2.TryHit();
            var answer3 = oceanPoint3.TryHit();

            // assert
            answer1.Should().NotBeNull();
            answer1.Reply.Should().Be(Reply.Hit);
            answer1.ShipLength.Should().Be(3);

            answer2.Should().NotBeNull();
            answer2.Reply.Should().Be(Reply.Hit);
            answer2.ShipLength.Should().Be(3);

            answer21.Should().NotBeNull();
            answer21.Reply.Should().Be(Reply.Hit);
            answer21.ShipLength.Should().Be(3);

            answer22.Should().NotBeNull();
            answer22.Reply.Should().Be(Reply.Hit);
            answer22.ShipLength.Should().Be(3);

            answer3.Should().NotBeNull();
            answer3.Reply.Should().Be(Reply.Sunk);
            answer3.ShipLength.Should().Be(3);
        }

        [Fact]
        public void ShouldReturnHitAndThenSunk()
        {
            // arrange
            var ship = new Ship(3);
            var oceanPoint1 = new OceanPoint();
            var oceanPoint2 = new OceanPoint();
            var oceanPoint3 = new OceanPoint();

            oceanPoint1.Put(ship);
            oceanPoint2.Put(ship);
            oceanPoint3.Put(ship);

            // act
            var answer1 = oceanPoint1.TryHit();
            var answer2 = oceanPoint2.TryHit();
            var answer3 = oceanPoint3.TryHit();

            // assert
            answer1.Should().NotBeNull();
            answer1.Reply.Should().Be(Reply.Hit);
            answer1.ShipLength.Should().Be(3);

            answer2.Should().NotBeNull();
            answer2.Reply.Should().Be(Reply.Hit);
            answer2.ShipLength.Should().Be(3);

            answer3.Should().NotBeNull();
            answer3.Reply.Should().Be(Reply.Sunk);
            answer3.ShipLength.Should().Be(3);
        }

        [Fact]
        public void ShouldReturnHitAndThenSunkAndThenMiss()
        {
            // arrange
            var ship = new Ship(2);
            var oceanPoint1 = new OceanPoint();
            var oceanPoint2 = new OceanPoint();
            var oceanPoint3 = new OceanPoint();

            oceanPoint1.Put(ship);
            oceanPoint2.Put(ship);

            // act
            var answer1 = oceanPoint1.TryHit();
            var answer2 = oceanPoint2.TryHit();
            var answer3 = oceanPoint3.TryHit();

            // assert
            answer1.Should().NotBeNull();
            answer1.Reply.Should().Be(Reply.Hit);
            answer1.ShipLength.Should().Be(2);

            answer2.Should().NotBeNull();
            answer2.Reply.Should().Be(Reply.Sunk);
            answer2.ShipLength.Should().Be(2);

            answer3.Should().NotBeNull();
            answer3.Reply.Should().Be(Reply.Miss);
            answer3.ShipLength.Should().Be(0);
        }

        [Fact]
        public void ShouldReturnSunkFor1LengthShip()
        {
            // arrange
            var oceanPoint = new OceanPoint();
            oceanPoint.Put(new Ship(1));

            // act
            var answer = oceanPoint.TryHit();

            // assert
            answer.Should().NotBeNull();
            answer.Reply.Should().Be(Reply.Sunk);
            answer.ShipLength.Should().Be(1);
        }

        [Fact]
        public void ShouldCloneOceanPoint()
        {
            // arrange
            var oceanPoint = new OceanPoint();
            oceanPoint.Put(new Ship(1));
            _ = oceanPoint.TryHit();
            var clonedOceanPoint = new OceanPoint(oceanPoint);

            // act
            var result = oceanPoint.Equals(clonedOceanPoint);

            // assert
            result.Should().BeTrue();
            oceanPoint.GetHashCode().Should().Be(clonedOceanPoint.GetHashCode());
        }

        [Fact]
        public void ShouldCompareTheOceanPoints()
        {
            // act
            var oceanPoint1 = new OceanPoint();
            oceanPoint1.Put(new Ship(1));
            _ = oceanPoint1.TryHit();

            var oceanPoint2 = new OceanPoint();
            oceanPoint2.Put(new Ship(1));
            _ = oceanPoint2.TryHit();

            // act
            var result = oceanPoint1.Equals(oceanPoint2);

            // assert
            result.Should().BeTrue();
            oceanPoint1.GetHashCode().Should().Be(oceanPoint2.GetHashCode());
        }

        [Fact]
        public void ShouldCompareDifferentOceanPoints()
        {
            // act
            var oceanPoint1 = new OceanPoint();
            oceanPoint1.Put(new Ship(1));
            _ = oceanPoint1.TryHit();

            var oceanPoint2 = new OceanPoint();
            oceanPoint2.Put(new Ship(1));

            // act
            var result = oceanPoint1.Equals(oceanPoint2);

            // assert
            result.Should().BeFalse();
            oceanPoint1.GetHashCode().Should().NotBe(oceanPoint2.GetHashCode());
        }

        [Fact]
        public void ShouldCompareOceanPointWithNull() 
        {
            // act
            var oceanPoint = new OceanPoint();
            
            // act
            var result = oceanPoint.Equals(null);

            // assert
            result.Should().BeFalse();
        }
    }
}
