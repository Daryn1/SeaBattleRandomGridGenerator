using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SeaBattleRandomGridGenerator;
using SeaBattleRandomGridGenerator.Ships;

namespace SeaBattleRandomGridGeneratorTests
{
    [TestFixture]
    public class ShipBuilderTests
    {
        public static IEnumerable<TestCaseData> BuildTestCases
        {
            get
            {
                yield return 
                    new TestCaseData(
                        new OneDeckShip(), 
                        new List<Cell>
                        {
                            new Cell(0, 0)
                        });
                yield return
                    new TestCaseData(
                        new TwoDeckShip(ShipRotation._0),
                        new List<Cell>
                        {
                            new Cell(0, 0), new Cell(0, 1)
                        });
                yield return
                    new TestCaseData(
                        new TwoDeckShip(ShipRotation._90),
                        new List<Cell>
                        {
                            new Cell(0, 0), new Cell(1, 0)
                        });
                yield return
                    new TestCaseData(
                        new ThreeDeckShip(ShipShape.Line, ShipRotation._0),
                        new List<Cell>
                        {
                            new Cell(0, 0), new Cell(0, 1), new Cell(0, 2)
                        });
                yield return
                    new TestCaseData(
                        new ThreeDeckShip(ShipShape.Line, ShipRotation._90),
                        new List<Cell>
                        {
                            new Cell(0, 0), new Cell(1, 0), new Cell(2, 0)
                        });
                yield return
                    new TestCaseData(
                        new ThreeDeckShip(ShipShape.LShaped, ShipRotation._0),
                        new List<Cell>
                        {
                            new Cell(0, 0), new Cell(1, 0), new Cell(1, 1)
                        });
                yield return
                    new TestCaseData(
                        new ThreeDeckShip(ShipShape.LShaped, ShipRotation._90),
                        new List<Cell>
                        {
                            new Cell(0, 0), new Cell(0, -1), new Cell(1, -1)
                        });
                yield return 
                    new TestCaseData(
                        new FourDeckShip(ShipShape.Line, ShipRotation._0), 
                        new List<Cell>
                        {
                            new Cell(0, 0), new Cell(0, 1), new Cell(0, 2), new Cell(0, 3)
                        });
                yield return
                    new TestCaseData(
                        new FourDeckShip(ShipShape.Line, ShipRotation._90),
                        new List<Cell>
                        {
                            new Cell(0, 0), new Cell(1, 0), new Cell(2, 0), new Cell(3, 0)
                        });
                yield return
                    new TestCaseData(
                        new FourDeckShip(ShipShape.Zigzag, ShipRotation._0),
                        new List<Cell>
                        {
                            new Cell(0, 0), new Cell(1, 0), new Cell(1, 1), new Cell(2, 1)
                        });
                yield return
                    new TestCaseData(
                        new FourDeckShip(ShipShape.Zigzag, ShipRotation._90),
                        new List<Cell>
                        {
                            new Cell(0, 0), new Cell(0, -1), new Cell(1, -1), new Cell(1, -2)
                        });
                yield return
                    new TestCaseData(
                        new FourDeckShip(ShipShape.ZigzagMirrored, ShipRotation._0),
                        new List<Cell>
                        {
                            new Cell(0, 0), new Cell(0, 1), new Cell(1, 1), new Cell(1, 2)
                        });
                yield return
                    new TestCaseData(
                        new FourDeckShip(ShipShape.ZigzagMirrored, ShipRotation._90),
                        new List<Cell>
                        {
                            new Cell(0, 0), new Cell(1, 0), new Cell(1, -1), new Cell(2, -1)
                        });
                yield return
                    new TestCaseData(
                        new FourDeckShip(ShipShape.LShaped, ShipRotation._0),
                        new List<Cell>
                        {
                            new Cell(0, 0), new Cell(1, 0), new Cell(2, 0), new Cell(2, 1)
                        });
                yield return
                    new TestCaseData(
                        new FourDeckShip(ShipShape.LShaped, ShipRotation._90),
                        new List<Cell>
                        {
                            new Cell(0, 0), new Cell(0, -1), new Cell(0, -2), new Cell(1, -2)
                        });
                yield return
                    new TestCaseData(
                        new FourDeckShip(ShipShape.LShaped, ShipRotation._180),
                        new List<Cell>
                        {
                            new Cell(0, 0), new Cell(-1, 0), new Cell(-2, 0), new Cell(-2, -1)
                        });
                yield return
                    new TestCaseData(
                        new FourDeckShip(ShipShape.LShaped, ShipRotation._270),
                        new List<Cell>
                        {
                            new Cell(0, 0), new Cell(0, 1), new Cell(0, 2), new Cell(-1, 2)
                        });
                yield return
                    new TestCaseData(
                        new FourDeckShip(ShipShape.LShapedMirrored, ShipRotation._0),
                        new List<Cell>
                        {
                            new Cell(0, 0), new Cell(0, 1), new Cell(0, 2), new Cell(1, 2)
                        });
                yield return
                    new TestCaseData(
                        new FourDeckShip(ShipShape.LShapedMirrored, ShipRotation._90),
                        new List<Cell>
                        {
                            new Cell(0, 0), new Cell(1, 0), new Cell(2, 0), new Cell(2, -1)
                        });
                yield return
                    new TestCaseData(
                        new FourDeckShip(ShipShape.LShapedMirrored, ShipRotation._180),
                        new List<Cell>
                        {
                            new Cell(0, 0), new Cell(0, -1), new Cell(0, -2), new Cell(-1, -2)
                        });
                yield return
                    new TestCaseData(
                        new FourDeckShip(ShipShape.LShapedMirrored, ShipRotation._270),
                        new List<Cell>
                        {
                            new Cell(0, 0), new Cell(-1, 0), new Cell(-2, 0), new Cell(-2, 1)
                        });
                yield return
                    new TestCaseData(
                        new FourDeckShip(ShipShape.Square, ShipRotation._0),
                        new List<Cell>
                        {
                            new Cell(0, 0), new Cell(0, 1), new Cell(1, 0), new Cell(1, 1)
                        });
            }
        }

        [Test]
        [TestCaseSource(nameof(BuildTestCases))]
        public void Build_InputShip_ReturnsShipRelativeCells(Ship ship, List<Cell> expectedResult)
        {
            var shipBuilder = new ShipBuilder();

            var result = shipBuilder.Build(ship);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
