using System;
using BattleShips.Domain;
using BattleShips.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace BattleShips.UnitTests
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
            var game = new Game(Columns, Rows);
            
            //Act
            Action act = () => game.Start(ships);
            
            //Assert
            act.Should().Throw<ShipOutOfBoardException>();
        }
        
        [Fact]
        public void NewGameWithOneShip_GameScore_OneShipsLeft()
        {
            //Arrange
            var ships = new[] { new Ship(new Coordinate[] { new(1, 1) }, "TestShip") };
            var game = new Game(Columns, Rows);
            game.Start(ships);

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
            var game = new Game(Columns, Rows);
            game.Start(ships);

            //Act
            game.HitAt(new Coordinate(1, 1));

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
            var game = new Game(Columns, Rows);
            game.Start(ships);

            //Act
            game.HitAt(new Coordinate(1, 1));

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
            var game = new Game(Columns, Rows);
            game.Start(ships);

            //Act
            game.HitAt(new Coordinate(1, 1));
            game.HitAt(new Coordinate(1, 2));

            //Assert
            game.GetScore().ShipsLeft.Should().Be(1);
        }
    }
}