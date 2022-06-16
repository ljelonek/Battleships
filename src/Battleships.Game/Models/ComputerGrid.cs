using Battleships.Game.Extensions;

namespace Battleships.Game.Models
{
    public sealed class ComputerGrid : Grid
    {
        private readonly Random _random = new(DateTime.UtcNow.Millisecond);

        public ComputerGrid(int size) : base(size) { }

        public void RegisterHit() => Hits--;

        public void PlaceShips(Ship[] ships)
        {
            foreach (var ship in ships)
            {
                var shipPlaced = false;
                while (!shipPlaced)
                {
                    var orientation = _random.NextBool();
                    var startingPoint = _random.NextPoint(Count, _width);
                    shipPlaced = TryPlaceShip(orientation, startingPoint, ship.Length);
                }
            }
        }

        private bool TryPlaceShip(bool orientation, Point startingPoint, int length)
        {
            var desiredPoints = orientation == Orientation.Horizontal
                ? startingPoint.CreateHorizontalRange(length, _width)
                : startingPoint.CreateVerticalRange(length, Count);
            if (desiredPoints.Length == 0 || desiredPoints.Any(IsTaken))
            {
                return false;
            }

            foreach (var desiredPoint in desiredPoints)
            {
                this[desiredPoint.RowIdentifier][desiredPoint.ColumnIndex] = Chars.Hit;
                Hits++;
            }

            return true;
        }

        private bool IsTaken(Point point) => GetHitOutcome(point) == Chars.Hit;
    }
}
