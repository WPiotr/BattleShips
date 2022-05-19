namespace BattleShips.Domain.ValueObjects
{
    public class Coordinate
    {
        public Coordinate(int column, int row)
        {
            Column = column;
            Row = row;
        }

        public int Column { get; }
        public int Row { get; }

        public override bool Equals(object obj)
        {
            if (obj is Coordinate coordinate)
            {
                return Column == coordinate.Column && Row == coordinate.Row;
            }

            return base.Equals(obj);
        }
    }
}