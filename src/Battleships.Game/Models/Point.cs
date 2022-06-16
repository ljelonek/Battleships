namespace Battleships.Game.Models
{
    public sealed record Point
    {
        public Point(char rowIdentifier, int columnIndex)
        {
            RowIdentifier = rowIdentifier;
            ColumnIndex = columnIndex;
        }

        public char RowIdentifier { get; }

        public int ColumnIndex { get; }

        public static bool TryParse(string? input, int size, out Point point)
        {
            point = new Point(default, default);
            var maxRowIdentifier = Chars.A + size;
            var maxColumnIndex = default(int) + size;
            var maxInput = $"{maxRowIdentifier}{maxColumnIndex}";
            if (string.IsNullOrWhiteSpace(input) || input.Length > maxInput.Length)
            {
                return false;
            }

            var inputRowIdentifier = input[0];
            var inputColumnIndex = int.TryParse(input[1..], out var result) ? result - 1 : default;
            if (IsInRange(inputRowIdentifier, Chars.A, maxRowIdentifier)
                && IsInRange(inputColumnIndex, default, maxColumnIndex))
            {
                point = new Point(inputRowIdentifier, inputColumnIndex);

                return true;
            }

            return false;
        }

        public Point[] CreateHorizontalRange(int length, int size) =>
            GetRange(ColumnIndex, length, default, size).Select(x => new Point(RowIdentifier, x)).ToArray();

        public Point[] CreateVerticalRange(int length, int size) =>
            GetRange(RowIdentifier, length, Chars.A, size).Select(x => new Point((char)x, ColumnIndex)).ToArray();

        private static bool IsInRange(int value, int minimum, int maximum) => value < maximum && value >= minimum;

        private static IEnumerable<int> GetRange(int start, int length, int minValue, int size)
        {
            var upperBound = start + length <= minValue + size ? Direction.Ascending : Direction.None;
            var lowerBound = start - length >= minValue ? Direction.Descending : Direction.None;

            return (upperBound + lowerBound) switch
            {
                Direction.AscendingDescending or Direction.Ascending => Enumerable.Range(start, length),
                Direction.Descending => Enumerable.Range(start - length, length),
                _ => Enumerable.Empty<int>(),
            };
        }
    }
}
