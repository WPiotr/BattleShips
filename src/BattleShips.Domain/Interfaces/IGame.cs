using BattleShips.Domain.ValueObjects;

namespace BattleShips.Domain.Interfaces
{
    public interface IGame
    {
        void Start(params (int shipSize, string shipName)[] shipDefinitions);
        GameScore GetScore();
        void HitAt(Coordinate coordinate);
    }
}