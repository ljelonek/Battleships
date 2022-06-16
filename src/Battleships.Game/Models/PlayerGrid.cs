namespace Battleships.Game.Models
{
    public sealed class PlayerGrid : Grid
    {
        private readonly ComputerGrid _computerGrid;
        private readonly int _paddingWidth;

        public PlayerGrid(ComputerGrid computerGrid, int paddingWidth) : base(computerGrid.Count)
        {
            _computerGrid = computerGrid;
            _paddingWidth = paddingWidth;
        }

        public void PrintToConsole()
        {
            Console.Clear();
            var columnIdentifiers = Enumerable.Range(default, _width).Select(GetColumnIdentifier);
            var columnHeaderPadding = Chars.Space.ToString();
            Console.WriteLine(columnHeaderPadding + string.Join(Chars.Space, columnIdentifiers));
            foreach (var row in this)
            {
                var formattedRowValues = row.Value.Select(x => FormatInput(x));
                Console.WriteLine(row.Key + string.Join(Chars.Space, formattedRowValues));
            }
        }

        public override char GetHitOutcome(Point point)
        {
            if (IsNotEmpty(point))
            {
                return Chars.Duplicate;
            }

            Hits++;
            return _computerGrid.GetHitOutcome(point) switch
            {
                Chars.Empty => this[point.RowIdentifier][point.ColumnIndex] = Chars.Miss,
                Chars.Hit => HandleHit(point),
                _ => Chars.Duplicate
            };
        }

        private char HandleHit(Point point)
        {
            _computerGrid.RegisterHit();
            this[point.RowIdentifier][point.ColumnIndex] = Chars.Hit;

            return _computerGrid.Hits == 0 ? Chars.End : Chars.Hit;
        }

        private bool IsNotEmpty(Point point) => base.GetHitOutcome(point) != Chars.Empty;

        private string GetColumnIdentifier(int index) => FormatInput(index + 1);

        private string FormatInput(object input) => $"{input}".PadLeft(_paddingWidth, Chars.Space);
    }
}
