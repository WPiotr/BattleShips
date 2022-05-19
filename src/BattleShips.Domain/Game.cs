using System.Collections.Generic;
using System.Linq;
using BattleShips.Domain.ValueObjects;

namespace BattleShips.Domain
{
    public class Game
    {
        private readonly int _maxColumn;
        private readonly int _maxRow;
        private Ship[] _ships;
        private readonly IList<Coordinate> _hits;

        public Game(int maxColumn, int maxRow)
        {
            _maxColumn = maxColumn;
            _maxRow = maxRow;
            _hits = new List<Coordinate>();
        }

        public void Start(params Ship[] ships)
        {
            ValidateShips(ships);
            _ships = ships;
        }

        public GameScore GetScore()
        {
            var shipsLeft = _ships.Count(ship => !ship.Coordinates.All(_hits.Contains));

            return new GameScore(shipsLeft);
        }

        public void HitAt(Coordinate coordinate)
        {
            _hits.Add(coordinate);
        }

        private void ValidateShips(Ship[] ships)
        {
            void ValidateShip(Ship ship)
            {
                var shipCoordinates = ship.Coordinates;
                var coordinatesOutOfBoard = shipCoordinates
                    .Any(shipCoordinate => shipCoordinate.Column > _maxColumn || shipCoordinate.Row > _maxRow);
                if (coordinatesOutOfBoard)
                {
                    throw new ShipOutOfBoardException(ship);
                }
            }

            foreach (var ship in ships)
            {
                ValidateShip(ship);
            }
        }
    }
}