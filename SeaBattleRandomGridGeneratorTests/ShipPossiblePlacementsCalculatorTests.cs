using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using SeaBattleRandomGridGenerator;
using SeaBattleRandomGridGenerator.Interfaces;
using SeaBattleRandomGridGenerator.Ships;

namespace SeaBattleRandomGridGeneratorTests
{
    [TestFixture]
    public class ShipPossiblePlacementsCalculatorTests
    {
        public static IEnumerable<TestCaseData> CalculatePossiblePlacementsTestCases1
        {
            get
            {
                yield return
                    new TestCaseData(
                        new OneDeckShip(),
                        new List<Cell>
                        {
                            new Cell(0, 0)
                        }, 
                        new List<List<Cell>> 
                        {
                            new List<Cell> { new Cell(0, 0) }, 
                            new List<Cell> { new Cell(0, 1) }, 
                            new List<Cell> { new Cell(0, 2) }, 
                            new List<Cell> { new Cell(0, 3) }
                        });
                yield return 
                    new TestCaseData(
                        new TwoDeckShip(ShipRotation._0), 
                        new List<Cell> 
                        {
                            new Cell(0, 0),
                            new Cell(0, 1)
                        },
                        new List<List<Cell>>
                        {
                            new List<Cell>
                            {
                                new Cell(0, 0),
                                new Cell(0, 1)
                            },
                            new List<Cell>
                            {
                                new Cell(0, 1),
                                new Cell(0, 2)
                            },
                            new List<Cell>
                            {
                                new Cell(0, 2),
                                new Cell(0, 3)
                            }
                        });
                yield return 
                    new TestCaseData(
                        new ThreeDeckShip(ShipShape.Line, ShipRotation._0),
                        new List<Cell>
                        {
                            new Cell(0, 0),
                            new Cell(0, 1),
                            new Cell(0, 2)
                        },
                        new List<List<Cell>>
                        {
                            new List<Cell>
                            {
                                new Cell(0, 0),
                                new Cell(0, 1),
                                new Cell(0, 2)
                            },
                            new List<Cell>
                            {
                                new Cell(0, 1),
                                new Cell(0, 2),
                                new Cell(0, 3)
                            }
                        });
                yield return 
                    new TestCaseData(
                        new FourDeckShip(ShipShape.Line, ShipRotation._0),
                        new List<Cell>
                        {
                            new Cell(0, 0),
                            new Cell(0, 1),
                            new Cell(0, 2),
                            new Cell(0, 3)
                        },
                        new List<List<Cell>>
                        {
                            new List<Cell>
                            {
                                new Cell(0, 0),
                                new Cell(0, 1),
                                new Cell(0, 2),
                                new Cell(0, 3)
                            }
                        });
            }
        }

        public static IEnumerable<TestCaseData> CalculatePossiblePlacementsTestCases2
        {
            get
            {
                yield return 
                    new TestCaseData(
                        new OneDeckShip(),
                        new List<Cell>
                        {
                            new Cell(0, 0)
                        },
                        100);
                yield return 
                    new TestCaseData(
                        new TwoDeckShip(ShipRotation._0),
                        new List<Cell>
                        {
                            new Cell(0, 0),
                            new Cell(0, 1)
                        },
                        90);
                yield return 
                    new TestCaseData(
                        new ThreeDeckShip(ShipShape.Line, ShipRotation._0),
                        new List<Cell>
                        {
                            new Cell(0, 0),
                            new Cell(0, 1),
                            new Cell(0, 2)
                        },
                        80);
                yield return 
                    new TestCaseData(
                        new FourDeckShip(ShipShape.Line, ShipRotation._0),
                        new List<Cell>
                        {
                            new Cell(0, 0),
                            new Cell(0, 1),
                            new Cell(0, 2),
                            new Cell(0, 3)
                        },
                        70);
            }
        }

        [Test]
        [TestCaseSource(nameof(CalculatePossiblePlacementsTestCases1))]
        public void CalculatePossiblePlacements_InputGridWithOnlyFourCellsForPlacement_ReturnsAllPossiblePlacements(
            Ship ship, List<Cell> shipRelativeCells, List<List<Cell>> expectedResult)
        {
            var shipBuilderStub = new Mock<IShipBuilder>();
            shipBuilderStub.Setup(s => s.Build(ship)).Returns(shipRelativeCells);
            var gridWithOnlyFourCellsForPlacement = GetGridWithOnlyFourCellsForPlacement();
            var shipPossiblePlacementsCalculator = new ShipPossiblePlacementsCalculator(shipBuilderStub.Object);

            var result = shipPossiblePlacementsCalculator
                .CalculatePossiblePlacements(ship, gridWithOnlyFourCellsForPlacement).ToList();

            shipBuilderStub.Verify(s => s.Build(ship), Times.Once);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCaseSource(nameof(CalculatePossiblePlacementsTestCases2))]
        public void CalculatePossiblePlacements_InputEmptyGrid_ReturnsAllPossiblePlacements(
            Ship ship, List<Cell> shipRelativeCells, int expectedNumberOfPossiblePlacements)
        {
            var shipBuilderStub = new Mock<IShipBuilder>();
            shipBuilderStub.Setup(s => s.Build(ship)).Returns(shipRelativeCells);
            var emptyGrid = EmptyGridGenerator.GenerateEmptyGridOfSize(Constants.GridSize);
            var shipPossiblePlacementsCalculator = new ShipPossiblePlacementsCalculator(shipBuilderStub.Object);

            var result = shipPossiblePlacementsCalculator.CalculatePossiblePlacements(ship, emptyGrid).ToList();

            shipBuilderStub.Verify(s => s.Build(ship), Times.Once);
            Assert.That(result.Count, Is.EqualTo(expectedNumberOfPossiblePlacements));
        }

        private bool[][] GetGridWithOnlyFourCellsForPlacement()
        {
            var grid = new bool[Constants.GridSize][];

            for (var i = 0; i < grid.Length; ++i)
            {
                var row = new bool[Constants.GridSize];

                for (var j = 0; j < row.Length; j++)
                {
                    if (i < 2 && j < 5)
                    {
                        row[j] = false;
                    }
                    else
                    {
                        row[j] = true;
                    }
                }

                grid[i] = row;
            }

            return grid;
        }
    }
}