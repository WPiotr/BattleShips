using System;
using System.Linq;

namespace BattleShips.Domain.ValueObjects
{
    public class Ship
    {
        public string Name { get; }

        public Coordinate[] Coordinates { get; }

        public Ship(Coordinate[] coordinates, string name)
        {
            ValidateCoordinates(coordinates);
            Coordinates = coordinates;
            Name = name;
        }

        private void ValidateCoordinates(Coordinate[] coordinates)
        {
            if (!coordinates.Any())
            {
                throw new Exception("Ship must have at leas one coordinate");
            }

            if (coordinates.Any(c => c.Column < 0 || c.Row < 0))
            {
                throw new Exception("Ship must have positive coordinates");
            }

            if (coordinates.Distinct().Count() < coordinates.Length)
            {
                throw new Exception("Ship need to have unique coordinates");
            }
        }
    }
}