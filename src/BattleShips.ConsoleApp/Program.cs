using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using BattleShips.ConsoleApp.Utils;
using BattleShips.Domain;
using BattleShips.Domain.ValueObjects;

namespace BattleShips.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            const int maxColumn = 10;
            const int maxRow = 10;
            var game = new Game(maxColumn, maxRow, new ShipGenerator());

            game.Start(new[]
            {
                (5, "Battleship"),
                (4, "Destroyer 1"),
                (4, "Destroyer 2")
            });

            while (true)
            {
                var gameScore = game.GetScore();

                if (gameScore.ShipsLeft == 0)
                {
                    break;
                }

                PrintBoard(gameScore.Hits, maxColumn, maxRow);
                var input = Console.ReadLine();
                if (!CoordinateParser.TryParse(input, out var column, out var row))
                {
                    continue;
                }
                
                game.HitAt(column, row);
            }
        }

        private static void PrintBoard((int column, int row, CellStatus status)[] gameScoreHits, int maxColumn, int maxRow)
        {
            char ConvertToLetter(int number)
            {
                return (char)(number + 64);
            }
            
            var stringBuilder = new StringBuilder();
            
            for (int rowNumber = 1; rowNumber <= maxRow; rowNumber++)
            {
                for (int columnNumber = 1; columnNumber <= maxColumn; columnNumber++)
                {
                    var gameHit = gameScoreHits
                        .FirstOrDefault(score => score.column == columnNumber
                                                 && score.row == rowNumber);
                    if (gameHit == default)
                    {
                        stringBuilder.Append($"[{ConvertToLetter(columnNumber)}{rowNumber}]".PadRight(6));
                    }
                    else
                    {
                        string statusSign;
                        switch (gameHit.status)
                        {
                            case CellStatus.Hit:
                                statusSign = "X";
                                break;
                            case CellStatus.Miss:
                                statusSign = "O";
                                break;
                            case CellStatus.Sink:
                                statusSign = "S";
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        stringBuilder.Append($"[{statusSign}]".PadRight(6));
                    }
                }

                stringBuilder.Append(Environment.NewLine);
            }
            Console.WriteLine(stringBuilder.ToString());
        }
    }
}