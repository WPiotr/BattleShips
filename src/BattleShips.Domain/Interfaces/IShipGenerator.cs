using BattleShips.Domain.ValueObjects;

namespace BattleShips.Domain.Interfaces
{
    public interface IShipGenerator
    {
        Ship[] GenerateShips((int, string)[] shipDefinitions, int maxColumn, int maxRow);
    }
}