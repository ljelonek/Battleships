namespace Battleships.Game.Models
{
    public class Ship
    {
        public Ship(IEnumerable<Point> coordinates)
        {
            State = coordinates.ToDictionary(x => x, _ => Chars.Active);
        }

        public Dictionary<Point, char> State { get; }

        public int Health => State.Values.Count(x => x == Chars.Active);
    }
}
