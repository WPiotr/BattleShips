using BattleShips.ConsoleApp.Utils;
using FluentAssertions;
using Xunit;

namespace BattleShips.UnitTests.ConsoleApp
{
    public class CoordinateParserTests
    {
        [Theory]
        [InlineData("A1", 1, 1)]
        [InlineData("B1", 2, 1)]
        [InlineData("C1", 3, 1)]
        [InlineData("D1", 4, 1)]
        [InlineData("A5", 1, 5)]
        [InlineData("A6", 1, 6)]
        [InlineData(" A6", 1, 6)]
        [InlineData(" A6 ", 1, 6)]
        public void TryParse_CorrectValue_ReturnsTrue_And_Coordinate(string input, int expectedColumn, int expectedRow)
        {
            //Act
            var result = CoordinateParser.TryParse(input, out var column, out var row);

            //Assert
            result.Should().BeTrue();
            column.Should().Be(expectedColumn);
            row.Should().Be(expectedRow);
        }
        
        [Theory]
        [InlineData("1")]
        [InlineData("41")]
        [InlineData(";1")]
        [InlineData(";)12")]
        public void TryParse_CorrectValue_ReturnsFalse(string input)
        {
            //Act
            var result = CoordinateParser.TryParse(input, out var column, out var row);

            //Assert
            result.Should().BeFalse();
        }
    }
}