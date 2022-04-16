namespace Battleships.Domain.Players
{
    using Battleships.Domain.Grids;
    using System;

    public class ComputerPlayer : Player
    {
        public ComputerPlayer(string playerName) : base(playerName) { }

        public override Point CallOutPointOnTargetingGrid()
        {
            var random = new Random();

            return new Point(
                random.Next(0, Grid.Size),
                random.Next(0, Grid.Size));
        }
    }
}
