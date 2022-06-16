namespace Battleships.Game.Models
{
    public class StrikeResult
    {
        public StrikeResult(char result, Point affectedPoint) : this(result, new[] { affectedPoint }) { }

        public StrikeResult(char result, IEnumerable<Point> affectedCoordinates)
        {
            Result = result;
            AffectedCoordinates = affectedCoordinates.ToArray();
        }

        public char Result { get; }

        public Point[] AffectedCoordinates { get; }

        public static StrikeResult Miss(Point affectedPoint) => new(Chars.Miss, affectedPoint);

        public static StrikeResult Hit(Point affectedPoint) => new(Chars.Hit, affectedPoint);

        public static StrikeResult Sunk(IEnumerable<Point> affectedCoordinates) => new(Chars.Sunk, affectedCoordinates);

        public static StrikeResult End(Point affectedPoint) => new(Chars.End, affectedPoint);
    }
}
