using System.Collections.Generic;
using SeaBattleRandomGridGenerator.Ships;

namespace SeaBattleRandomGridGenerator.Interfaces
{
    public interface IShipPossiblePlacementsCalculator
    {
        IEnumerable<List<Cell>> CalculatePossiblePlacements(Ship ship, bool[][] grid);
    }
}