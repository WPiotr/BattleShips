using System;
using System.Collections.Generic;
using System.Linq;
using BattleShips.Domain.Interfaces;
using BattleShips.Domain.ValueObjects;

namespace BattleShips.Domain
{
    public class Game : IGame
    {
        private readonly int _maxColumn;
        private readonly int _maxRow;
        private readonly IShipGenerator _shipGenerator;
        private Ship[] _ships;
        private readonly IList<Coordinate> _hits;

        public Game(int maxColumn, int maxRow, IShipGenerator shipGenerator)
        {
            _maxColumn = maxColumn;
            _maxRow = maxRow;
            _shipGenerator = shipGenerator;
            _hits = new List<Coordinate>();
        }

        public void Start((int shipSize, string shipName)[] shipDefinitions)
        {
            var ships = _shipGenerator.GenerateShips(shipDefinitions, _maxColumn, _maxRow);

            ValidateShips(ships);
            _ships = ships;
        }

        public GameScore GetScore()
        {
            var shipsLeft = _ships.Count(ship => !ship.Coordinates.All(_hits.Contains));
            (int column, int row, CellStatus)[] hits = _hits.Select(coordinate =>
                (coordinate.Column, coordinate.Row, GetCellStatus(coordinate))).ToArray();
            return new GameScore(shipsLeft, hits);
        }

        private CellStatus GetCellStatus(Coordinate coordinate)
        {
            var ship = _ships.FirstOrDefault(ship => ship.Coordinates.Contains(coordinate)); 
            if (ship == null)
            {
                return CellStatus.Miss;
            }

            return ship.Coordinates.All(_hits.Contains) ? CellStatus.Sink : CellStatus.Hit;
        }

        public void HitAt(int column, int row)
        {
            var coordinate = new Coordinate(column, row);
            ValidateCoordinate(coordinate);
            _hits.Add(coordinate);
        }

        private void ValidateCoordinate(Coordinate coordinate)
        {
            if (coordinate.Column < 0 && coordinate.Column >= _maxColumn)
            {
                throw new Exception($"Column should be greater than zero and lower than {_maxColumn}");
            }

            if (coordinate.Row < 0 && coordinate.Row >= _maxRow)
            {
                throw new Exception($"Row should be greater than zero and lower than {_maxRow}");
            }
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