using BattleShips.Domain;
using FluentAssertions;
using Xunit;

namespace BattleShips.UnitTests
{
    public class GameTests
    {
        [Fact]
        public void NewGameWithOneShip_GameScore_OneShipsLeft()
        {
            //Arrange
            var ships = new[] { new Ship() };
            var game = new Game();
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
            var ships = new[] { new Ship(new Coordinate(1, 1)) };
            var game = new Game();
            game.Start(ships);

            //Act
            game.HitAt(new Coordinate(1, 1));

            //Assert
            game.GetScore().ShipsLeft.Should().Be(0);
        }
    }
}