using Battleships.Game.Extensions;

namespace Battleships.Game.Models
{
    public class EnemyFleet
    {
        private readonly Grid _grid;
        private readonly Random _random = new(DateTime.UtcNow.Millisecond);

        public EnemyFleet(IEnumerable<ShipOrder> shipOrders, Grid grid)
        {
            _grid = grid;
            var shipLengths = new List<int>();
            foreach (var shipOrder in shipOrders)
            {
                shipLengths.AddRange(Enumerable.Repeat(shipOrder.Length, shipOrder.Count));
            }
            Ships = new List<Ship>();
            GenerateShips(shipLengths);
        }

        public StrikeResult StrikeAt(Point point)
        {
            var damagedShip = Ships.Find(x => x.State.ContainsKey(point));
            if (damagedShip == null)
            {
                return StrikeResult.Miss(point);
            }

            damagedShip.State[point] = Chars.Hit;
            if (damagedShip.Health == 0)
            {
                Ships.Remove(damagedShip);

                return StrikeResult.Sunk(damagedShip.State.Keys);
            }

            return StrikeResult.Hit(point);
        }

        public bool HasShips => Ships.Count > 0;

        private List<Ship> Ships { get; }

        private void GenerateShips(IEnumerable<int> shipLengths)
        {
            foreach (var shipLength in shipLengths)
            {
                var shipPlaced = false;
                while (!shipPlaced)
                {
                    var orientation = _random.NextBool();
                    var startingPoint = _random.NextPoint(_grid.Length, _grid.Width);
                    shipPlaced = TryPlaceShip(orientation, startingPoint, shipLength);
                }
            }
        }

        private bool TryPlaceShip(bool orientation, Point startingPoint, int length)
        {
            var desiredPoints = orientation == Orientation.Horizontal
                ? startingPoint.CreateHorizontalRange(length, _grid.Width)
                : startingPoint.CreateVerticalRange(length, _grid.Length);
            if (desiredPoints.Length == 0 || desiredPoints.Any(IsTaken))
            {
                return false;
            }

            Ships.Add(new Ship(desiredPoints));

            return true;
        }

        private bool IsTaken(Point point) => Ships.Any(x => x.State.ContainsKey(point));
    }
}
