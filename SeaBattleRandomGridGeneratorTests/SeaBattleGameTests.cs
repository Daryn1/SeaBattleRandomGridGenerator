using Moq;
using NUnit.Framework;
using SeaBattleRandomGridGenerator;
using SeaBattleRandomGridGenerator.Interfaces;
using SeaBattleRandomGridGenerator.Ships;

namespace SeaBattleRandomGridGeneratorTests
{
    [TestFixture]
    public class SeaBattleGameTests
    {
        [Test]
        public void GenerateRandomGrid_Generates10ShipsAndCallPlaceRandomlyForEveryShip()
        {
            var randomShipGeneratorStub = new Mock<IShipGenerator>();
            var fourDeckShip = new FourDeckShip(ShipShape.Line, ShipRotation._0);
            var threeDeckShip = new ThreeDeckShip(ShipShape.Line, ShipRotation._0);
            var twoDeckShip = new TwoDeckShip(ShipRotation._0);
            var oneDeckShip = new OneDeckShip();
            randomShipGeneratorStub.Setup(s => s.GenerateShipOfSize(4)).Returns(fourDeckShip);
            randomShipGeneratorStub.Setup(s => s.GenerateShipOfSize(3)).Returns(threeDeckShip);
            randomShipGeneratorStub.Setup(s => s.GenerateShipOfSize(2)).Returns(twoDeckShip);
            randomShipGeneratorStub.Setup(s => s.GenerateShipOfSize(1)).Returns(oneDeckShip);
            var shipPlacerStub = new Mock<IShipPlacer>();
            var emptyGrid = EmptyGridGenerator.GenerateEmptyGridOfSize(Constants.GridSize);
            var seaBattleGame = new SeaBattleGame(randomShipGeneratorStub.Object, shipPlacerStub.Object);

            seaBattleGame.GenerateRandomGrid();

            Assert.That(seaBattleGame.Grid, Is.EqualTo(emptyGrid));
            randomShipGeneratorStub.Verify(generator => generator.GenerateShipOfSize(4), Times.Exactly(1));
            randomShipGeneratorStub.Verify(generator => generator.GenerateShipOfSize(3), Times.Exactly(2));
            randomShipGeneratorStub.Verify(generator => generator.GenerateShipOfSize(2), Times.Exactly(3));
            randomShipGeneratorStub.Verify(generator => generator.GenerateShipOfSize(1), Times.Exactly(4));
            shipPlacerStub.Verify(placer => placer.PlaceRandomly(fourDeckShip, It.IsAny<bool[][]>()), Times.Exactly(1));
            shipPlacerStub.Verify(placer => placer.PlaceRandomly(threeDeckShip, It.IsAny<bool[][]>()), Times.Exactly(2));
            shipPlacerStub.Verify(placer => placer.PlaceRandomly(twoDeckShip, It.IsAny<bool[][]>()), Times.Exactly(3));
            shipPlacerStub.Verify(placer => placer.PlaceRandomly(oneDeckShip, It.IsAny<bool[][]>()), Times.Exactly(4));
        }
        
        [Test]
        public void ResetGrid_GridIsNotEmpty_MakeGridEmpty()
        {
            var seaBattleGame = new SeaBattleGame(null, null);
            var emptyGrid = EmptyGridGenerator.GenerateEmptyGridOfSize(Constants.GridSize);
            seaBattleGame.Grid[0][0] = true;

            seaBattleGame.ResetGrid();

            Assert.That(seaBattleGame.Grid, Is.EqualTo(emptyGrid));
        }
    }
}