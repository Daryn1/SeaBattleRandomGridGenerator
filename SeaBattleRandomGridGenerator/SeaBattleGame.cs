using System.Collections.Generic;
using SeaBattleRandomGridGenerator.Ships;

namespace SeaBattleRandomGridGenerator
{
    class SeaBattleGame
    {
        public bool[][] Grid { get; set; } = new bool[GridSize][];

        private const int GridSize = 10;

        private RandomShipGenerator randomShipGenerator = new RandomShipGenerator();

        private ShipPlacer shipPlacer = new ShipPlacer();

        private List<Ship> ships;

        public SeaBattleGame()
        {
            ResetGrid();
        }

        public void ResetGrid()
        {
            for (var i = 0; i < GridSize; ++i)
            {
                Grid[i] = new bool[] { false, false, false, false, false, false, false, false, false, false };
            }
        }

        public void GenerateRandomGrid()
        {
            ResetGrid();
            ships = GenerateRandomShips();

            foreach (var ship in ships)
            {
                shipPlacer.PlaceRandomly(ship, Grid);
            }
        }

        private List<Ship> GenerateRandomShips()
        {
            return new List<Ship>
            {
                randomShipGenerator.Generate(shipSize:4),

                randomShipGenerator.Generate(shipSize:3),
                randomShipGenerator.Generate(shipSize:3),

                randomShipGenerator.Generate(shipSize:2),
                randomShipGenerator.Generate(shipSize:2),
                randomShipGenerator.Generate(shipSize:2),

                randomShipGenerator.Generate(shipSize:1),
                randomShipGenerator.Generate(shipSize:1),
                randomShipGenerator.Generate(shipSize:1),
                randomShipGenerator.Generate(shipSize:1)
            };
        }
    }
}
