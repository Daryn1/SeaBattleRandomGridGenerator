using System.Collections.Generic;
using SeaBattleRandomGridGenerator.Interfaces;
using SeaBattleRandomGridGenerator.Ships;

namespace SeaBattleRandomGridGenerator
{
    public class ShipBuilder : IShipBuilder
    {
        public List<Cell> Build(Ship ship)
        {
            var relativeShipCells = new List<Cell>();

            switch (ship.Shape)
            {
                case ShipShape.Line:
                    for (var x = 0; x < ship.Size; x++)
                    {
                        relativeShipCells.Add(new Cell(0, x));
                    }

                    break;
                case ShipShape.LShaped:
                    var numberOfRightMoves = 1;
                    var numberOfDownwardMoves = ship.Size - 1;

                    for (var y = 0; y < numberOfDownwardMoves; y++)
                    {
                        relativeShipCells.Add(new Cell(y, 0));
                    }

                    relativeShipCells.Add(new Cell(numberOfDownwardMoves - 1, numberOfRightMoves));
                    break;
                case ShipShape.LShapedMirrored:
                    relativeShipCells.Add(new Cell(0, 0));
                    relativeShipCells.Add(new Cell(0, 1));
                    relativeShipCells.Add(new Cell(0, 2));
                    relativeShipCells.Add(new Cell(1, 2));
                    break;
                case ShipShape.Zigzag:
                    relativeShipCells.Add(new Cell(0, 0));
                    relativeShipCells.Add(new Cell(1, 0));
                    relativeShipCells.Add(new Cell(1, 1));
                    relativeShipCells.Add(new Cell(2, 1));
                    break;
                case ShipShape.ZigzagMirrored:
                    relativeShipCells.Add(new Cell(0, 0));
                    relativeShipCells.Add(new Cell(0, 1));
                    relativeShipCells.Add(new Cell(1, 1));
                    relativeShipCells.Add(new Cell(1, 2));
                    break;
                case ShipShape.Square:
                    relativeShipCells.Add(new Cell(0, 0));
                    relativeShipCells.Add(new Cell(0, 1));
                    relativeShipCells.Add(new Cell(1, 0));
                    relativeShipCells.Add(new Cell(1, 1));
                    break;
            }

            // Поворот корабля.
            for (var index = 0; index < relativeShipCells.Count; index++)
            {
                relativeShipCells[index] = RotateCell(ship.Rotation, relativeShipCells[index]);
            }

            return relativeShipCells;
        }

        private Cell RotateCell(ShipRotation rotation, Cell cell)
        {
            var rotatedCell = new Cell(0, 0);

            switch (rotation)
            {
                case ShipRotation._0:
                    return cell;
                case ShipRotation._90:
                    rotatedCell.X = -cell.Y;
                    rotatedCell.Y = cell.X;
                    break;
                case ShipRotation._180:
                    rotatedCell.X = -cell.X;
                    rotatedCell.Y = -cell.Y;
                    break;
                case ShipRotation._270:
                    rotatedCell.X = cell.Y;
                    rotatedCell.Y = -cell.X;
                    break;
            }

            return rotatedCell;
        }
    }
}
