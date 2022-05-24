namespace BattleShips.Domain.ValueObjects
{
    public class GameScore
    {
        public GameScore(int shipsLeft, (int column, int row, CellStatus)[] hits)
        {
            ShipsLeft = shipsLeft;
            Hits = hits;
        }

        public int ShipsLeft { get; }
        public (int column, int row, CellStatus)[] Hits { get; }
    }
}