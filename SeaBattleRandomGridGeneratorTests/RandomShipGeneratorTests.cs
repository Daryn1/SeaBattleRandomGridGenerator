using System;
using Moq;
using NUnit.Framework;
using SeaBattleRandomGridGenerator;
using SeaBattleRandomGridGenerator.Interfaces;
using SeaBattleRandomGridGenerator.Ships;

namespace SeaBattleRandomGridGeneratorTests
{
    [TestFixture]
    public class RandomShipGeneratorTests
    {
        [Test]
        public void GenerateShipOfSize_Input1_ReturnsOneDeckShip()
        {
            var randomShipGenerator = new RandomShipGenerator(null);

            var result = randomShipGenerator.GenerateShipOfSize(1);

            Assert.That(result, Is.TypeOf<OneDeckShip>());
        }

        [Test]
        [TestCase(ShipRotation._0)]
        [TestCase(ShipRotation._90)]
        public void GenerateShipOfSize_Input2_ReturnsTwoDeckShipWithRotationGeneratedByRandom(
            ShipRotation shipRotationGeneratedByRandom)
        {
            var randomIndexGeneratorStub = new Mock<IRandomGenerator>();
            var possibleRotationsForTwoDeckShip = new[] { ShipRotation._0, ShipRotation._90 };
            randomIndexGeneratorStub.Setup(r => r.GetRandomElementFromArrayAsInt(possibleRotationsForTwoDeckShip))
                .Returns((int)shipRotationGeneratedByRandom);
            var randomShipGenerator = new RandomShipGenerator(randomIndexGeneratorStub.Object);

            var result = randomShipGenerator.GenerateShipOfSize(2);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.TypeOf<TwoDeckShip>());
                Assert.That(result.Rotation, Is.EqualTo(shipRotationGeneratedByRandom));
            });
        }

        [Test]
        [TestCase(ShipShape.Line, ShipRotation._0)]
        [TestCase(ShipShape.LShaped, ShipRotation._0)]
        [TestCase(ShipShape.Line, ShipRotation._90)]
        [TestCase(ShipShape.LShaped, ShipRotation._90)]
        public void GenerateShipOfSize_Input3_ReturnsThreeDeckShipWithShapeAndRotationGeneratedByRandom(
            ShipShape shipShapeGeneratedByRandom, ShipRotation shipRotationGeneratedByRandom)
        {
            var randomIndexGeneratorStub = new Mock<IRandomGenerator>();
            var possibleShapesForThreeDeckShip = new[] { ShipShape.Line, ShipShape.LShaped };
            var possibleRotationsForThreeDeckShip = new[] { ShipRotation._0, ShipRotation._90 };
            randomIndexGeneratorStub.Setup(r => r.GetRandomElementFromArrayAsInt(possibleShapesForThreeDeckShip))
                .Returns((int)shipShapeGeneratedByRandom);
            randomIndexGeneratorStub.Setup(r => r.GetRandomElementFromArrayAsInt(possibleRotationsForThreeDeckShip))
                .Returns((int)shipRotationGeneratedByRandom);
            var randomShipGenerator = new RandomShipGenerator(randomIndexGeneratorStub.Object);

            var result = randomShipGenerator.GenerateShipOfSize(3);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.TypeOf<ThreeDeckShip>());
                Assert.That(result.Shape, Is.EqualTo(shipShapeGeneratedByRandom));
                Assert.That(result.Rotation, Is.EqualTo(shipRotationGeneratedByRandom));
            });
        }
        
        [Test]
        [TestCase(ShipShape.Line, ShipRotation._0)]
        [TestCase(ShipShape.Line, ShipRotation._90)]
        [TestCase(ShipShape.Zigzag, ShipRotation._0)]
        [TestCase(ShipShape.Zigzag, ShipRotation._90)]
        [TestCase(ShipShape.ZigzagMirrored, ShipRotation._0)]
        [TestCase(ShipShape.ZigzagMirrored, ShipRotation._90)]
        public void
            GenerateShipOfSize_Input4RandomReturnsLineOrZigzagShipShape_ReturnsFourDeckShipWithShapeAndRotationGeneratedByRandom(
                ShipShape shipShapeGeneratedByRandom, ShipRotation shipRotationGeneratedByRandom)
        {
            var randomIndexGeneratorStub = new Mock<IRandomGenerator>();
            var fourDeckShipShapes = Enum.GetValues(typeof(ShipShape));
            var lineOrZigzagShapeFourDeckShipRotations = new[] { ShipRotation._0, ShipRotation._90 };
            randomIndexGeneratorStub.Setup(r => r.GetRandomElementFromArrayAsInt(fourDeckShipShapes))
                .Returns((int)shipShapeGeneratedByRandom);
            randomIndexGeneratorStub.Setup(r => r.GetRandomElementFromArrayAsInt(lineOrZigzagShapeFourDeckShipRotations))
                .Returns((int)shipRotationGeneratedByRandom);
            var randomShipGenerator = new RandomShipGenerator(randomIndexGeneratorStub.Object);

            var result = randomShipGenerator.GenerateShipOfSize(4);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.TypeOf<FourDeckShip>());
                Assert.That(result.Shape, Is.EqualTo(shipShapeGeneratedByRandom));
                Assert.That(result.Rotation, Is.EqualTo(shipRotationGeneratedByRandom));
            });
        }

        [Test]
        [TestCase(ShipShape.LShaped, ShipRotation._0)]
        [TestCase(ShipShape.LShaped, ShipRotation._90)]
        [TestCase(ShipShape.LShaped, ShipRotation._180)]
        [TestCase(ShipShape.LShaped, ShipRotation._270)]
        [TestCase(ShipShape.LShapedMirrored, ShipRotation._0)]
        [TestCase(ShipShape.LShapedMirrored, ShipRotation._90)]
        [TestCase(ShipShape.LShapedMirrored, ShipRotation._180)]
        [TestCase(ShipShape.LShapedMirrored, ShipRotation._270)]
        public void
            GenerateShipOfSize_Input4RandomReturnsLShapedShipShape_ReturnsFourDeckShipWithShapeAndRotationGeneratedByRandom(
                ShipShape shipShapeGeneratedByRandom, ShipRotation shipRotationGeneratedByRandom)
        {
            var randomIndexGeneratorStub = new Mock<IRandomGenerator>();
            var fourDeckShipShapes = Enum.GetValues(typeof(ShipShape));
            var fourDeckShipRotations = Enum.GetValues(typeof(ShipRotation));
            randomIndexGeneratorStub.Setup(r => r.GetRandomElementFromArrayAsInt(fourDeckShipShapes))
                .Returns((int)shipShapeGeneratedByRandom);
            randomIndexGeneratorStub.Setup(r => r.GetRandomElementFromArrayAsInt(fourDeckShipRotations))
                .Returns((int)shipRotationGeneratedByRandom);
            var randomShipGenerator = new RandomShipGenerator(randomIndexGeneratorStub.Object);

            var result = randomShipGenerator.GenerateShipOfSize(4);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.TypeOf<FourDeckShip>());
                Assert.That(result.Shape, Is.EqualTo(shipShapeGeneratedByRandom));
                Assert.That(result.Rotation, Is.EqualTo(shipRotationGeneratedByRandom));
            });
        }

        [Test]
        public void
            GenerateShipOfSize_Input4RandomReturnsSquareShipShape_ReturnsFourDeckShipWith0RotationAndSquareShipShape()
        {
            var randomIndexGeneratorStub = new Mock<IRandomGenerator>();
            var fourDeckShipShapes = Enum.GetValues(typeof(ShipShape));
            var squareFourDeckShipRotations = new[] { ShipRotation._0 };
            randomIndexGeneratorStub.Setup(r => r.GetRandomElementFromArrayAsInt(fourDeckShipShapes))
                .Returns((int)ShipShape.Square);
            randomIndexGeneratorStub.Setup(r => r.GetRandomElementFromArrayAsInt(squareFourDeckShipRotations))
                .Returns((int)ShipRotation._0);
            var randomShipGenerator = new RandomShipGenerator(randomIndexGeneratorStub.Object);

            var result = randomShipGenerator.GenerateShipOfSize(4);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.TypeOf<FourDeckShip>());
                Assert.That(result.Shape, Is.EqualTo(ShipShape.Square));
                Assert.That(result.Rotation, Is.EqualTo(ShipRotation._0));
            });
        }
    }
}
