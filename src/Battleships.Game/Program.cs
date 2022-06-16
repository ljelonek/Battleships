using Battleships.Game.Models;

namespace Battleships.Game
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var configuration = Configuration.Build(args);
            var grid = new Grid(configuration.GridSize);
            var shipOrders = new[]
            {
                new ShipOrder(length: 5, count: 1),
                new ShipOrder(length: 4, count: 2)
            };
            var enemyFleet = new EnemyFleet(shipOrders, grid);
            var playerGrid = new PlayerGrid(configuration.PaddingWidth, grid);
            Engine.Start(playerGrid, enemyFleet, grid);
        }
    }
}