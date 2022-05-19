namespace BattleShips.Domain.ValueObjects
{
    public class Ship
    {
        public Coordinate[] Coordinates { get; }

        public Ship(Coordinate[] coordinates)
        {
            Coordinates = coordinates;
        }
    }
}