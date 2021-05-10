using System;
using System.Linq;

namespace SeaBattleRandomGridGenerator
{
    static class Tests
    {
        public static void TestShipBuilding()
        {
            var shipBuilder = new ShipBuilder();
            var randomShipGenerator = new RandomShipGenerator();
            var ship = randomShipGenerator.Generate(shipSize:4);
            var startingCell = new Cell(3, 3);
            var relativeShipCells = shipBuilder.Build(ship);
            var absoluteShipCells = relativeShipCells
                .Select(shipCell => new Cell(shipCell.Y + startingCell.Y, shipCell.X + startingCell.X)).ToList();

            for (var y = 0; y < 7; y++)
            {
                for (var x = 0; x < 7; x++)
                {
                    Console.Write(absoluteShipCells.Any(cell => cell.X == x && cell.Y == y) ? 1 : 0);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine(ship.Shape.ToString());
            Console.WriteLine(ship.Rotation.ToString());
        }
    }
}
