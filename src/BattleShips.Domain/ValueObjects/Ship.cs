namespace BattleShips.Domain.ValueObjects
{
    public class Ship
    {
        public string Name { get; }
        
        public Coordinate[] Coordinates { get; }

        public Ship(Coordinate[] coordinates, string name)
        {
            Coordinates = coordinates;
            Name = name;
        }
    }
}