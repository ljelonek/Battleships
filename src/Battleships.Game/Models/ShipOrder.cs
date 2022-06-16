namespace Battleships.Game.Models
{
    public record struct ShipOrder
    {
        public ShipOrder(int length, int count)
        {
            Length = length;
            Count = count;
        }

        public int Length { get; }

        public int Count { get; }
    }
}
