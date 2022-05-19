using System.Collections.Generic;
using System.Linq;
using BattleShips.Domain.ValueObjects;

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
}