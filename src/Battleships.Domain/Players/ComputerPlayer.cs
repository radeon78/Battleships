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
                random.Next(0, _oceanGrid.Size),
                random.Next(0, _oceanGrid.Size));
        }

        public override void PrintOceanGrid() 
        {
            // We don't have to print anything for computer player
        }

        public override void PrintTargetingGrind()
        {
            // We don't have to print anything for computer player
        }
    }
}
