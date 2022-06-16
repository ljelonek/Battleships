namespace Battleships.Game
{
    public sealed class Configuration
    {
        private const uint MaximumSize = 26;
        private const uint MaximumPaddingWidth = 10;

        private Configuration() { }

        public Configuration(uint gridSize, uint paddingWidth)
        {
            GridSize = gridSize <= MaximumSize
                ? (int)gridSize : throw new ArgumentOutOfRangeException(nameof(gridSize));
            PaddingWidth = paddingWidth <= MaximumPaddingWidth
                ? (int)paddingWidth : throw new ArgumentOutOfRangeException(nameof(paddingWidth));
        }

        public static Configuration Default => new();

        public int GridSize { get; } = 10;

        public int PaddingWidth { get; } = 2;
    }
}
