namespace Battleships.Domain.Players
{
    using Battleships.Domain.Common;
    using Battleships.Domain.Extensions;
    using Battleships.Domain.Grids;
    using System;

    public class HumanPlayer : Player
    {
        private readonly Func<string, StartPoint> _getPlaceShipStartPoint;
        private readonly Action<string> _placeShipNotAllowed;

        public HumanPlayer(
            string playerName,
            Func<string, StartPoint> getPlaceShipStartPoint,
            Action<string> placeShipNotAllowed) : base(playerName)
        {
            _getPlaceShipStartPoint = getPlaceShipStartPoint;
            _placeShipNotAllowed = placeShipNotAllowed;
        }

        public override void PlaceShipsOnGrid()
        {
            _allowedShips.ForEach(ship =>
            {
                var startPoint = _getPlaceShipStartPoint(string.Format(Resource.PlaceShipMessage, _playerName, ship));

                Result result;
                while ((result = _oceanGrid.TryPlaceShip(startPoint, ship)).IsFailure)
                {
                    _placeShipNotAllowed(result.ErrorMessage);
                    startPoint = _getPlaceShipStartPoint(string.Format(Resource.PlaceShipMessage, _playerName, ship));
                }
            });
        }
    }
}
