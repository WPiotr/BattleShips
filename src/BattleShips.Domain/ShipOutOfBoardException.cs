using System;
using BattleShips.Domain.ValueObjects;

namespace BattleShips.Domain
{
    public class ShipOutOfBoardException : Exception
    {
        public ShipOutOfBoardException(Ship ship) : base($"Ship {ship.Name} is outside the board.")
        {
        }
    }
}