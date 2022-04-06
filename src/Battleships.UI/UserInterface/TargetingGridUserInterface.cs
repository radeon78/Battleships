namespace Battleships.UI.UserInterface
{
    using Battleships.Domain.Grids;
    using System;

    public static class TargetingGridUserInterface
    {
        public static Point CallOutPointOnTargetingGrid(string message)
        {
            Console.WriteLine($"\n{message}");
            var column = CommonUserInterface.GetColumn();
            var row = CommonUserInterface.GetRow();
            
            return new Point(column, row);
        }
    }
}
