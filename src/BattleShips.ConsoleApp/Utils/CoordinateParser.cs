using System.Linq;

namespace BattleShips.ConsoleApp.Utils
{
    public static class CoordinateParser
    {
        public static bool TryParse(string input, out int? column, out int? row)
        {
            column = null;
            row = null;
            var trimmedInput = input.Trim();
            var columnLetter = trimmedInput[0];
            if (!char.IsLetter(columnLetter))
            {
                return false;
            }

            var rowPart = trimmedInput[1..];
            if (rowPart.Count() > 2)
            {
                return false;
            }

            if (!int.TryParse(rowPart, out var parsedRowNumber))
            {
                return false;
            }

            column = columnLetter - 64;
            row = parsedRowNumber;
            return true;
        }
    }
}