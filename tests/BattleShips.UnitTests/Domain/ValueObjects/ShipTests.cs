using System;
using BattleShips.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace BattleShips.UnitTests.Domain.ValueObjects
{
    public class ShipTests
    {
        [Theory]
        [MemberData(nameof(NewShip_InvalidCoordinates_ThrowException_Data))]
        public void NewShip_InvalidCoordinates_ThrowException(Coordinate[] coordinates)
        {
            //Act
            Action act = () => new Ship(coordinates, "TestShip");

            //Assert
            act.Should().Throw<Exception>();
        }

        public static TheoryData<Coordinate[]> NewShip_InvalidCoordinates_ThrowException_Data()
        {
            var data = new TheoryData<Coordinate[]>();

            data.Add(new[]
            {
                new Coordinate(-1, 1),
                new Coordinate(1, 2)
            });

            data.Add(new[]
            {
                new Coordinate(1, 1),
                new Coordinate(1, 1)
            });

            data.Add(new Coordinate[]
            {
            });

            return data;
        }


        [Theory]
        [MemberData(nameof(NewShip_ValidCoordinates_NotThrowException_Data))]
        public void NewShip_ValidCoordinates_NotThrowException(Coordinate[] coordinates)
        {
            //Act
            Action act = () => new Ship(coordinates, "TestShip");

            //Assert
            act.Should().NotThrow();
        }

        public static TheoryData<Coordinate[]> NewShip_ValidCoordinates_NotThrowException_Data()
        {
            var data = new TheoryData<Coordinate[]>();

            data.Add(new[]
            {
                new Coordinate(1, 1),
                new Coordinate(1, 2),
                new Coordinate(1, 3)
            });

            data.Add(new[]
            {
                new Coordinate(1, 4),
                new Coordinate(1, 3),
                new Coordinate(1, 5)
            });

            data.Add(new[]
            {
                new Coordinate(1, 1),
                new Coordinate(2, 1),
                new Coordinate(3, 1),
            });

            data.Add(new[]
            {
                new Coordinate(5, 1),
                new Coordinate(4, 1),
                new Coordinate(6, 1),
            });

            return data;
        }
    }
}