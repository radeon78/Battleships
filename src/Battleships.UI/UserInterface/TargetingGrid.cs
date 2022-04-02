namespace Battleships.UI.UserInterface
{
    using Battleships.Domain.Grids;
    using System;

    public static class TargetingGrid
    {
        public static Point CallOutPointOnTargetingGrid(string message)
        {
            Console.WriteLine($"\n{message}");
            var column = Common.GetColumn();
            var row = Common.GetRow();
            
            return new Point(column, row);
        }
    }
}
