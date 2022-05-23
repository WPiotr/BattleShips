using System;
using System.Collections.Generic;
using System.Linq;
using BattleShips.Domain.Interfaces;
using BattleShips.Domain.ValueObjects;

namespace BattleShips.Domain
{
    public class ShipGenerator : IShipGenerator
    {
        private readonly Random _random = new();

        public Ship[] GenerateShips((int, string)[] shipDefinitions, int maxColumn, int maxRow)
        {
            var result = new List<Ship>(shipDefinitions.Length);
            foreach (var (size, name) in shipDefinitions)
            {
                var generatedShipCoordinates = result.SelectMany(ship => ship.Coordinates);
                var newShipCoordinates = GenerateShipCoordinates(maxColumn, maxRow, size, generatedShipCoordinates);

                result.Add(new Ship(newShipCoordinates, name));
            }

            return result.ToArray();
        }

        private Coordinate[] GenerateShipCoordinates(int maxColumn, int maxRow,
            int size, IEnumerable<Coordinate> generatedShipCoordinates)
        {
            Coordinate[] newShipCoordinates;
            do
            {
                var direction = _random.NextDouble();
                newShipCoordinates = direction > 0.5
                    ? GenerateHorizontalCoordinates(size, maxColumn, maxRow)
                    : GenerateVerticalCoordinates(size, maxColumn, maxRow);
            } while (generatedShipCoordinates.Any(newShipCoordinates.Contains));

            return newShipCoordinates;
        }

        private Coordinate[] GenerateVerticalCoordinates(int size, int maxColumn, int maxRow)
        {
            var coordinates = new Coordinate[size];
            var startColumn = _random.Next(1, maxColumn);
            var startRow = _random.Next(1, maxRow - size);
            coordinates[0] = new Coordinate(startColumn, startRow);
            for (var i = 1; i < size; i++)
            {
                coordinates[i] = new Coordinate(startColumn, startRow + i);
            }

            return coordinates;
        }

        private Coordinate[] GenerateHorizontalCoordinates(int size, int maxColumn, int maxRow)
        {
            var coordinates = new Coordinate[size];
            var startColumn = _random.Next(1, maxColumn - size);
            var startRow = _random.Next(1, maxRow);
            coordinates[0] = new Coordinate(startColumn, startRow);
            for (var i = 1; i < size; i++)
            {
                coordinates[i] = new Coordinate(startColumn + i, startRow);
            }

            return coordinates;
        }
    }
}