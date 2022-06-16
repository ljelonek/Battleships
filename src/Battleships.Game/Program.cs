using Battleships.Game.Models;

namespace Battleships.Game
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var configuration = GetConfiguration(args);
            var computerGrid = new ComputerGrid(configuration.GridSize);
            var ships = new[] { new Ship(5), new Ship(4), new Ship(4) };
            computerGrid.PlaceShips(ships);
            var playerGrid = new PlayerGrid(computerGrid, configuration.PaddingWidth);
            var userOutput = Language.WaitingForInput;
            while (true)
            {
                playerGrid.PrintToConsole();
                Console.WriteLine(userOutput);
                var input = Console.ReadLine();
                if (!Point.TryParse(input, configuration.GridSize, out var point))
                {
                    userOutput = Language.InvalidInput;
                    continue;
                }

                var hitOutcome = playerGrid.GetHitOutcome(point);
                userOutput = hitOutcome switch
                {
                    Chars.Miss => Language.Miss,
                    Chars.Hit => Language.Hit,
                    Chars.Duplicate => Language.Duplicate,
                    _ => Language.InvalidOutcome
                };
                if (hitOutcome == Chars.End)
                {
                    playerGrid.PrintToConsole();
                    Console.WriteLine($"Game won! Hits: {playerGrid.Hits}/100.");
                    break;
                }
            }
        }

        private static Configuration GetConfiguration(string[] args)
        {
            if (args.Length == 2)
            {
                return uint.TryParse(args[0], out var gridSize)
                    ? uint.TryParse(args[1], out var paddingWidth)
                        ? new Configuration(gridSize, paddingWidth)
                        : throw new ArgumentException($"{nameof(args)}[1]")
                    : throw new ArgumentException($"{nameof(args)}[0]");
            }

            return Configuration.Default;
        }
    }
}