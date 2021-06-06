using System.Collections.Generic;
using SeaBattleRandomGridGenerator.Interfaces;
using SeaBattleRandomGridGenerator.Ships;

namespace SeaBattleRandomGridGenerator
{
    public class SeaBattleGame
    {
        public bool[][] Grid { get; set; }

        private IShipGenerator randomShipGenerator;

        private IShipPlacer shipPlacer;

        private List<Ship> ships;

        public SeaBattleGame(IShipGenerator randomShipGenerator, IShipPlacer shipPlacer)
        {
            this.randomShipGenerator = randomShipGenerator;
            this.shipPlacer = shipPlacer;
            ResetGrid();
        }

        public void ResetGrid()
        {
            Grid = EmptyGridGenerator.GenerateEmptyGridOfSize(Constants.GridSize);
        }

        public void GenerateRandomGrid()
        {
            ResetGrid();
            ships = Generate10RandomShips();

            foreach (var ship in ships)
            {
                shipPlacer.PlaceRandomly(ship, Grid);
            }
        }

        private List<Ship> Generate10RandomShips()
        {
            return new List<Ship>
            {
                randomShipGenerator.GenerateShipOfSize(shipSize:4),

                randomShipGenerator.GenerateShipOfSize(shipSize:3),
                randomShipGenerator.GenerateShipOfSize(shipSize:3),

                randomShipGenerator.GenerateShipOfSize(shipSize:2),
                randomShipGenerator.GenerateShipOfSize(shipSize:2),
                randomShipGenerator.GenerateShipOfSize(shipSize:2),

                randomShipGenerator.GenerateShipOfSize(shipSize:1),
                randomShipGenerator.GenerateShipOfSize(shipSize:1),
                randomShipGenerator.GenerateShipOfSize(shipSize:1),
                randomShipGenerator.GenerateShipOfSize(shipSize:1)
            };
        }
    }
}
