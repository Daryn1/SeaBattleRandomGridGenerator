using System;

namespace SeaBattleRandomGridGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var seaBattleGame = new SeaBattleGame();
            var gridDrawer = new GridDrawer();

            do
            {
                seaBattleGame.GenerateRandomGrid();
                gridDrawer.Draw(seaBattleGame.Grid);
                Console.WriteLine();
                Console.WriteLine("Нажмите любую клавишу, чтобы сгенерировать новую сетку.");
                Console.WriteLine("Нажмите ESC, чтобы выйти.");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);

            // Tests.TestShipBuilding();
        }
    }
}
