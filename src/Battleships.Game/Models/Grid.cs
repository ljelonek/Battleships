namespace Battleships.Game.Models
{
    public abstract class Grid : Dictionary<char, char[]>
    {
        protected readonly int _width;

        protected Grid(int size) : this(size, size) { }

        private Grid(int width, int length)
        {
            _width = width;
            foreach (var key in Enumerable.Range(default, length))
            {
                this[GetRowIdentifier(key)] = Enumerable.Repeat(Chars.Empty, _width).ToArray();
            }
        }

        public int Hits { get; protected set; }

        public virtual char GetHitOutcome(Point point) => this[point.RowIdentifier][point.ColumnIndex];

        private static char GetRowIdentifier(int index) => (char)(Chars.A + index);
    }
}
