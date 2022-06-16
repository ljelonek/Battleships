namespace Battleships.Game.Models
{
    public sealed class PlayerGrid : Dictionary<char, char[]>
    {
        private readonly Grid _grid;
        private readonly int _paddingWidth;

        public PlayerGrid(int paddingWidth, Grid grid)
        {
            _grid = grid;
            _paddingWidth = paddingWidth;
            foreach (var key in Enumerable.Range(default, grid.Length))
            {
                this[GetRowIdentifier(key)] = Enumerable.Repeat(Chars.Empty, _grid.Width).ToArray();
            }
        }

        public string Score => $"{Hits}/{_grid.Length * _grid.Width}";

        private int Hits { get; set; }

        public void PrintToConsole()
        {
            Console.Clear();
            var columnIdentifiers = Enumerable.Range(default, _grid.Width).Select(GetColumnIdentifier);
            var columnHeaderPadding = Chars.Space.ToString();
            Console.WriteLine(columnHeaderPadding + string.Join(Chars.Space, columnIdentifiers));
            foreach (var row in this)
            {
                var formattedRowValues = row.Value.Select(x => FormatInput(x));
                Console.WriteLine(row.Key + string.Join(Chars.Space, formattedRowValues));
            }
        }

        public bool IsAlreadyHit(Point point) => this[point.RowIdentifier][point.ColumnIndex] != Chars.Empty;

        public void RegisterStrikeResult(StrikeResult strikeResult)
        {
            Hits++;
            foreach (var point in strikeResult.AffectedCoordinates)
            {
                this[point.RowIdentifier][point.ColumnIndex] = strikeResult.Result;
            }
        }

        private static char GetRowIdentifier(int index) => (char)(Chars.A + index);

        private string GetColumnIdentifier(int index) => FormatInput(index + 1);

        private string FormatInput(object input) => $"{input}".PadLeft(_paddingWidth, Chars.Space);
    }
}
