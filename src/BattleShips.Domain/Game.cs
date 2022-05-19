using System.Collections.Generic;
using System.Linq;

namespace BattleShips.Domain
{
    public class Game
    {
        private Ship[] _ships;
        private readonly IList<Coordinate> _hits;

        public Game()
        {
            _hits = new List<Coordinate>();
        }

        public void Start(params Ship[] ships)
        {
            _ships = ships;
        }

        public GameScore GetScore()
        {
            var shipsLeft = _ships.Count(ship => !ship.Coordinates.All(_hits.Contains));

            return new GameScore(shipsLeft);
        }

        public void HitAt(Coordinate coordinate)
        {
            _hits.Add(coordinate);
        }
    }

    public class GameScore
    {
        public GameScore(int shipsLeft)
        {
            ShipsLeft = shipsLeft;
        }

        public int ShipsLeft { get; }
    }

    public class Ship
    {
        public Coordinate[] Coordinates { get; }

        public Ship(Coordinate[] coordinates)
        {
            Coordinates = coordinates;
        }
    }

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