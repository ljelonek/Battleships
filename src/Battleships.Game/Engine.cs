using Battleships.Game.Models;

namespace Battleships.Game
{
    internal static class Engine
    {
        public static void Start(PlayerGrid playerGrid, EnemyFleet enemyFleet, Grid grid)
        {
            var userOutput = Language.WaitingForInput;
            while (enemyFleet.HasShips)
            {
                playerGrid.PrintToConsole();
                Console.WriteLine(userOutput);
                var input = Console.ReadLine();
                if (!Point.TryParse(input, grid, out var point))
                {
                    userOutput = Language.InvalidInput;
                    continue;
                }

                if (playerGrid.IsAlreadyHit(point))
                {
                    userOutput = Language.Duplicate;
                    continue;
                }

                var strikeResult = enemyFleet.StrikeAt(point);
                playerGrid.RegisterStrikeResult(strikeResult);
                userOutput = strikeResult.Result switch
                {
                    Chars.Miss => Language.Miss,
                    Chars.Hit => Language.Hit,
                    Chars.Sunk => Language.Sunk,
                    _ => Language.InvalidOutcome
                };
            }

            playerGrid.PrintToConsole();
            Console.WriteLine($"Game won! Hits: {playerGrid.Score}.");
        }
    }
}
