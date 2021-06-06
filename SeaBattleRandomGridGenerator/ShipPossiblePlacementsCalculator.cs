using System.Collections.Generic;
using System.Linq;
using SeaBattleRandomGridGenerator.Interfaces;
using SeaBattleRandomGridGenerator.Ships;

namespace SeaBattleRandomGridGenerator
{
    public class ShipPossiblePlacementsCalculator : IShipPossiblePlacementsCalculator
    {
        private IShipBuilder shipBuilder;

        public ShipPossiblePlacementsCalculator(IShipBuilder shipBuilder)
        {
            this.shipBuilder = shipBuilder;
        }

        public IEnumerable<List<Cell>> CalculatePossiblePlacements(Ship ship, bool[][] grid)
        {
            var cellsForPlacement = GetCellsForPlacement(grid);
            var relativeShipCells = shipBuilder.Build(ship);

            for (var y = 0; y < grid.Length; y++)
            {
                for (var x = 0; x < grid.Length; x++)
                {
                    var startingCell = new Cell(y, x);
                    var shipCells = GetAbsoluteCells(relativeShipCells, startingCell);

                    if (CanShipBePlaced(cellsForPlacement, shipCells))
                    {
                        yield return shipCells;
                    }
                }
            }
        }

        private List<Cell> GetAbsoluteCells(List<Cell> relativeCells, Cell startingCell)
        {
            var absoluteShipCells = relativeCells
                .Select(shipCell => new Cell(shipCell.Y + startingCell.Y, shipCell.X + startingCell.X)).ToList();

            return absoluteShipCells;
        }

        private bool CanShipBePlaced(bool[][] cellsForPlacement, List<Cell> shipCells)
        {
            // Проверка выхода за границы массива.
            if (shipCells.Any(shipCell => shipCell.X < 0 || shipCell.Y < 0 || shipCell.X >= cellsForPlacement.Length ||
                                          shipCell.Y >= cellsForPlacement.Length))
            {
                return false;
            }

            return shipCells.All(cell => cellsForPlacement[cell.Y][cell.X]);
        }

        /// <summary>
        /// Возвращает карту клеток, на которые можно размещать корабли.
        /// </summary>
        private bool[][] GetCellsForPlacement(bool[][] grid)
        {
            var cellsForPlacement = new bool[grid.Length][];

            for (var i = 0; i < grid.Length; ++i)
            {
                cellsForPlacement[i] = new bool[] { true, true, true, true, true, true, true, true, true, true };
            }

            for (var y = 0; y < grid.Length; y++)
            {
                for (var x = 0; x < grid.Length; x++)
                {
                    if (grid[y][x])
                    {
                        // Помечаем занятую клетку и её соседей как недоступных для размещения.
                        for (var i = -1; i <= 1; i++)
                        {
                            for (var j = -1; j <= 1; j++)
                            {
                                var yNeighbor = y + i;
                                var xNeighbor = x + j;

                                // Проверка выхода индекса за границы массива.
                                if (yNeighbor >= 0 && yNeighbor < grid.Length && xNeighbor >= 0 &&
                                    xNeighbor < grid.Length)
                                {
                                    cellsForPlacement[yNeighbor][xNeighbor] = false;
                                }
                            }
                        }
                    }
                }
            }

            return cellsForPlacement;
        }
    }
}