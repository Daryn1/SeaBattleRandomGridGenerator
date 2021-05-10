using System;
using System.Collections.Generic;
using System.Linq;
using SeaBattleRandomGridGenerator.Ships;

namespace SeaBattleRandomGridGenerator
{
    class ShipPlacer
    {
        private Random random = new Random();

        private ShipBuilder shipBuilder = new ShipBuilder();

        public void PlaceRandomly(Ship ship, bool[][] grid)
        {
            var possiblePlacements = GetPossiblePlacements(ship, grid);
            var randomIndex = random.Next(possiblePlacements.Count);
            var startingCellOfShip = possiblePlacements[randomIndex];
            var shipCells = GetShipCells(ship, startingCellOfShip);

            foreach (var shipCell in shipCells)
            {
                grid[shipCell.Y][shipCell.X] = true;
            }
        }

        private List<Cell> GetPossiblePlacements(Ship ship, bool[][] grid)
        {
            var cellsForPlacement = GetCellsForPlacement(grid);
            var startingCellsOfShip = new List<Cell>();

            for (var y = 0; y < grid.Length; y++)
            {
                for (var x = 0; x < grid.Length; x++)
                {
                    var cell = new Cell(y, x);

                    if (CanShipBePlaced(ship, cellsForPlacement, cell))
                    {
                        startingCellsOfShip.Add(cell);
                    }
                }
            }

            return startingCellsOfShip;
        }

        private bool CanShipBePlaced(Ship ship, bool[][] cellsForPlacement, Cell startingCell)
        {
            var shipCells = GetShipCells(ship, startingCell);

            // Проверка выхода за границы массива.
            if (shipCells.Any(shipCell => shipCell.X < 0 || shipCell.Y < 0 || shipCell.X >= cellsForPlacement.Length ||
                                          shipCell.Y >= cellsForPlacement.Length))
            {
                return false;
            }

            return shipCells.All(cell => cellsForPlacement[cell.Y][cell.X]);
        }

        private List<Cell> GetShipCells(Ship ship, Cell startingCell)
        {
            var relativeShipCells = shipBuilder.Build(ship);
            var absoluteShipCells = relativeShipCells
                .Select(shipCell => new Cell(shipCell.Y + startingCell.Y, shipCell.X + startingCell.X)).ToList();

            return absoluteShipCells;
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
