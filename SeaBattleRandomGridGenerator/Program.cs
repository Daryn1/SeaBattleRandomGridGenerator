using System;
using System.Collections.Generic;

namespace SeaBattleRandomGridGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var randomIndexGenerator = new RandomGenerator();
            var shipBuilder = new ShipBuilder();
            var shipPossiblePlacementsCalculator = new ShipPossiblePlacementsCalculator(shipBuilder);
            var randomShipGenerator = new RandomShipGenerator(randomIndexGenerator);
            var shipPlacer = new ShipPlacer(randomIndexGenerator, shipPossiblePlacementsCalculator);
            var seaBattleGame = new SeaBattleGame(randomShipGenerator, shipPlacer);
            var gridDrawer = new GridDrawer();

            do
            {
                seaBattleGame.GenerateRandomGrid();
                gridDrawer.Draw(seaBattleGame.Grid);
                Console.WriteLine();
                Console.WriteLine("Нажмите любую клавишу, чтобы сгенерировать новую сетку.");
                Console.WriteLine("Нажмите ESC, чтобы выйти.");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }
    }
}
