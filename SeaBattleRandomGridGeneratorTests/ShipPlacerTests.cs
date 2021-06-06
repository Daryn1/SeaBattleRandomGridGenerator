using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using NUnit.Framework;
using SeaBattleRandomGridGenerator;
using SeaBattleRandomGridGenerator.Interfaces;
using SeaBattleRandomGridGenerator.Ships;

namespace SeaBattleRandomGridGeneratorTests
{
    [TestFixture]
    public class ShipPlacerTests
    {
        public static IEnumerable<TestCaseData> PlaceRandomlyTestCases
        {
            get
            {
                yield return new TestCaseData(new FourDeckShip(ShipShape.Line, ShipRotation._0), new List<List<Cell>> 
                {
                        new List<Cell> {new Cell(0, 0), new Cell(0, 1), new Cell(0, 2), new Cell(0, 3)},
                        new List<Cell> {new Cell(0, 1), new Cell(0, 2), new Cell(0, 3), new Cell(0, 4)}
                });
                yield return new TestCaseData(new FourDeckShip(ShipShape.Line, ShipRotation._90), new List<List<Cell>>
                {
                    new List<Cell> {new Cell(0, 0), new Cell(1, 0), new Cell(2, 0), new Cell(3, 0) }
                });
                yield return new TestCaseData(new FourDeckShip(ShipShape.LShaped, ShipRotation._0), new List<List<Cell>>
                {
                    new List<Cell> {new Cell(0, 0), new Cell(1, 0), new Cell(2, 0), new Cell(2, 1) }
                });
                yield return new TestCaseData(new ThreeDeckShip(ShipShape.Line, ShipRotation._0), new List<List<Cell>>
                {
                    new List<Cell> {new Cell(0, 0), new Cell(0, 1), new Cell(0, 2) }
                });
                yield return new TestCaseData(new TwoDeckShip(ShipRotation._0), new List<List<Cell>>
                {
                    new List<Cell> {new Cell(0, 0), new Cell(1, 0) }
                });
                yield return new TestCaseData(new OneDeckShip(), new List<List<Cell>>
                {
                    new List<Cell> {new Cell(0, 0) }
                });
            }
        }

        [Test]
        [TestCaseSource(nameof(PlaceRandomlyTestCases))]
        public void PlaceRandomly_GridIsEmptyAndRandomChoosesFirstPossiblePlacement_PlacedShipOnGrid(Ship ship,
            List<List<Cell>> possiblePlacements)
        {
            var randomIndexGeneratorStub = new Mock<IRandomGenerator>();
            var shipPossiblePlacementsCalculatorStub = new Mock<IShipPossiblePlacementsCalculator>();
            var modifiedGrid = EmptyGridGenerator.GenerateEmptyGridOfSize(Constants.GridSize);
            shipPossiblePlacementsCalculatorStub.Setup(s => s.CalculatePossiblePlacements(ship, modifiedGrid))
                .Returns(possiblePlacements);
            randomIndexGeneratorStub.Setup(r => r.GetRandomNumberFromRange(0, possiblePlacements.Count))
                .Returns((int minValue, int maxValue) => minValue);
            const int firstIndex = 0;
            var chosenPlacement = possiblePlacements[firstIndex];
            var shipPlacer = new ShipPlacer(randomIndexGeneratorStub.Object, shipPossiblePlacementsCalculatorStub.Object);
            var actualResult = EmptyGridGenerator.GenerateEmptyGridOfSize(Constants.GridSize);
            foreach (var cell in chosenPlacement)
            {
                actualResult[cell.Y][cell.X] = true;
            }

            shipPlacer.PlaceRandomly(ship, modifiedGrid);

            Assert.That(modifiedGrid, Is.EqualTo(actualResult));
        }
    }
}
