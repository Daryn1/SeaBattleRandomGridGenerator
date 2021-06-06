using System;
using System.Collections.Generic;
using System.Linq;
using SeaBattleRandomGridGenerator.Interfaces;
using SeaBattleRandomGridGenerator.Ships;

namespace SeaBattleRandomGridGenerator
{
    public class ShipPlacer : IShipPlacer
    {
        private IRandomGenerator randomGenerator;

        private IShipPossiblePlacementsCalculator shipPossiblePlacementsCalculator;

        public ShipPlacer(IRandomGenerator randomGenerator, IShipPossiblePlacementsCalculator shipPossiblePlacementsCalculator)
        {
            this.randomGenerator = randomGenerator;
            this.shipPossiblePlacementsCalculator = shipPossiblePlacementsCalculator;
        }

        public void PlaceRandomly(Ship ship, bool[][] grid)
        {
            var possiblePlacements = shipPossiblePlacementsCalculator.CalculatePossiblePlacements(ship, grid).ToList();
            var randomIndex = randomGenerator.GetRandomNumberFromRange(0, possiblePlacements.Count);
            var shipCells = possiblePlacements[randomIndex];

            foreach (var shipCell in shipCells)
            {
                grid[shipCell.Y][shipCell.X] = true;
            }
        }
    }
}
