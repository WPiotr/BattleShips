using System;
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
                var input = Console.ReadLine();
                if (!CoordinateParser.TryParse(input, out var column, out var row))
                {
                    continue;
                }
                
                game.HitAt(column, row);
                var gameScore = game.GetScore();

                if (gameScore.ShipsLeft == 0)
                {
                    break;
                }
            }
        }
    }
}