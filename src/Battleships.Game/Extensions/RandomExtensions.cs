using Battleships.Game.Models;

namespace Battleships.Game.Extensions
{
    public static class RandomExtensions
    {
        public static Point NextPoint(this Random random, int length, int width)
        {
            var randomRowIdentifier = random.Next(default, length) + Chars.A;
            var randomColumnIndex = random.Next(default, width);

            return new Point((char)randomRowIdentifier, randomColumnIndex);
        }

        public static bool NextBoolean(this Random random) => random.Next(default, 2) > 0;
    }
}
