using System.Linq;
using AutoFixture.Xunit2;
using BattleShips.Domain;
using BattleShips.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace BattleShips.UnitTests.Domain
{
    public class ShipGeneratorTests
    {
        [Theory]
        [AutoData]
        public void GenerateShips_OneShipGenerated_WithCorrectName_CorrectCoordinates(
            string expectedShipName)
        {
            //Arrange
            var maxColumn = 10;
            var maxRow = 10;
            var expectedShipSize = 3;
            var shipDefinitions = new (int, string)[] { (expectedShipSize, expectedShipName) };

            var shipGenerator = new ShipGenerator();

            //Act
            var ships = shipGenerator.GenerateShips(shipDefinitions, maxColumn, maxRow);

            //Assert
            ships.Should().HaveCount(shipDefinitions.Length);
            var ship = ships.First();
            ship.Name.Should().Be(expectedShipName);
            ship.Coordinates.Should().HaveCount(expectedShipSize);
            CoordinatesShouldBeOnTheBoard(ship, maxColumn, maxRow);
        }

        [Theory]
        [AutoData]
        public void GenerateShips_ManyShipGenerated_Should_MatchShipDefinitions_And_HasCorrectCoordinates(
            string[] expectedShipNames)
        {
            //Arrange
            var maxColumn = 10;
            var maxRow = 10;
            var expectedShipSize = 3;
            var shipDefinitions = expectedShipNames
                .Select(shipName => (expectedShipSize, shipName))
                .ToArray();

            var shipGenerator = new ShipGenerator();

            //Act
            var ships = shipGenerator.GenerateShips(shipDefinitions, maxColumn, maxRow);

            //Assert
            ships.Should().HaveCount(shipDefinitions.Length);
            foreach (var ship in ships)
            {
                var shipDefinition = shipDefinitions.FirstOrDefault(def => def.shipName == ship.Name);
                shipDefinition.Should().NotBeNull();
                ship.Coordinates.Should().HaveCount(shipDefinition.expectedShipSize);
                ship.Name.Should().Be(shipDefinition.shipName);
                CoordinatesShouldBeOnTheBoard(ship, maxColumn, maxRow);
            }
        }

        private static void CoordinatesShouldBeOnTheBoard(Ship ship, int maxColumn, int maxRow)
        {
            ship.Coordinates.Should().AllSatisfy(coordinate =>
            {
                coordinate.Column.Should().BeGreaterThan(0).And.BeLessThan(maxColumn);
                coordinate.Row.Should().BeGreaterThan(0).And.BeLessThan(maxRow);
            });
        }
    }
}