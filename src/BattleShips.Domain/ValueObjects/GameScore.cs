namespace BattleShips.Domain.ValueObjects
{
    public class GameScore
    {
        public GameScore(int shipsLeft)
        {
            ShipsLeft = shipsLeft;
        }

        public int ShipsLeft { get; }
    }
}