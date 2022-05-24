using System;
using BattleShips.Domain;
using BattleShips.Domain.Interfaces;
using BattleShips.Domain.ValueObjects;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace BattleShips.UnitTests.Domain
{
    public class GameTests
    {
        private const int Columns = 10;
        private const int Rows = 10;

        [Fact]
        public void StartGame_ShipsOutsideBoard_ThrowsShipOutOfBoardException()
        {
            //Arrange
            var ships = new[] { new Ship(new Coordinate[] { new(11, 11) }, "TestShip") };
            var shipDefinitions = new[] { (1, "testShip") };
            var shipGenerator = SetupShipGeneratorMock(shipDefinitions, ships);

            var game = new Game(Columns, Rows, shipGenerator);

            //Act
            Action act = () => game.Start(shipDefinitions);

            //Assert
            act.Should().Throw<ShipOutOfBoardException>();
        }

        [Fact]
        public void NewGameWithOneShip_GameScore_OneShipsLeft()
        {
            //Arrange
            var ships = new[] { new Ship(new Coordinate[] { new(1, 1) }, "TestShip") };
            var shipDefinitions = new[] { (1, "testShip") };
            var shipGenerator = SetupShipGeneratorMock(shipDefinitions, ships);

            var game = new Game(Columns, Rows, shipGenerator);
            game.Start(shipDefinitions);

            //Act
            var gameScore = game.GetScore();

            //Assert
            gameScore.ShipsLeft.Should().Be(ships.Length);
        }

        [Fact]
        public void NewGameWithOneShip_ShipHit_NoShipsLeft()
        {
            //Arrange
            var ships = new[] { new Ship(new Coordinate[] { new(1, 1) }, "TestShip") };
            var shipDefinitions = new[] { (1, "testShip") };
            var shipGenerator = SetupShipGeneratorMock(shipDefinitions, ships);

            var game = new Game(Columns, Rows, shipGenerator);
            game.Start(shipDefinitions);

            //Act
            game.HitAt(1, 1);

            //Assert
            game.GetScore().ShipsLeft.Should().Be(0);
        }

        [Fact]
        public void NewGameWithTwoShips_ShipHitButNotSink_TwoShipsLeft()
        {
            //Arrange
            var ships = new[]
            {
                new Ship(new Coordinate[] { new(1, 1), new(1, 2) }, "TestShip 1"),
                new Ship(new Coordinate[] { new(2, 2) }, "TestShip 2")
            };

            var shipDefinitions = new[] { (2, "TestShip 1"), (1, "TestShip 2") };
            var shipGenerator = SetupShipGeneratorMock(shipDefinitions, ships);


            var game = new Game(Columns, Rows, shipGenerator);
            game.Start(shipDefinitions);

            //Act
            game.HitAt(1, 1);

            //Assert
            game.GetScore().ShipsLeft.Should().Be(ships.Length);
        }

        [Fact]
        public void NewGameWithTwoShips_ShipHitTwiceAndSink_OneShipsLeft()
        {
            //Arrange
            var ships = new[]
            {
                new Ship(new Coordinate[] { new(1, 1), new(1, 2) }, "TestShip 1"),
                new Ship(new Coordinate[] { new(2, 2) }, "TestShip 2")
            };

            var shipDefinitions = new[] { (2, "testShip"), (1, "TestShip 2") };
            var shipGenerator = SetupShipGeneratorMock(shipDefinitions, ships);

            var game = new Game(Columns, Rows, shipGenerator);
            game.Start(shipDefinitions);

            //Act
            game.HitAt(1, 1);
            game.HitAt(1, 2);

            //Assert
            game.GetScore().ShipsLeft.Should().Be(1);
        }

        private static IShipGenerator SetupShipGeneratorMock((int, string)[] shipDefinitions, Ship[] ships)
        {
            var shipGenerator = Substitute.For<IShipGenerator>();
            shipGenerator.GenerateShips(shipDefinitions, Columns, Rows).Returns(ships);
            return shipGenerator;
        }
    }
}