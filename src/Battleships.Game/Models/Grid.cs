namespace Battleships.Game.Models
{
    public class Grid
    {
        public Grid(int size) : this(size, size) { }

        private Grid(int width, int length)
        {
            Width = width;
            Length = length;
        }

        public int Width { get; }

        public int Length { get; }
    }
}
