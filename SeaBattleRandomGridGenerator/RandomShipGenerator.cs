using System;
using SeaBattleRandomGridGenerator.Ships;

namespace SeaBattleRandomGridGenerator
{
    class RandomShipGenerator
    {
        private Random random = new Random();

        private const int NumberOfThreeDeckShipPossibleShapes = 2;

        private const int NumberOfThreeDeckShipPossibleRotations = 2;

        private const int NumberOfTwoDeckShipPossibleRotations = 2;

        public Ship Generate(int shipSize)
        {
            var shipShapes = Enum.GetValues(typeof(ShipShape));
            var shipRotations = Enum.GetValues(typeof(ShipRotation));

            switch (shipSize)
            {
                case 4:
                {
                    var randomType = (ShipShape)shipShapes.GetValue(random.Next(shipShapes.Length));

                    // Ограничиваем число возможных поворотов корабля, для того, чтобы один и тот же корабль
                    // не мог быть сгенерирован двумя разными комбинациями формы и поворота, в результате чего
                    // генерация кораблей будет производится равновероятно.
                        var NumberOfPossibleRotations = randomType switch
                    {
                        ShipShape.Line => 2,
                        ShipShape.LShaped => 4,
                        ShipShape.LShapedMirrored => 4,
                        ShipShape.Zigzag => 2,
                        ShipShape.ZigzagMirrored => 2,
                        ShipShape.Square => 1,
                        _ => throw new ArgumentOutOfRangeException(nameof(randomType))
                    };

                    var randomRotation = (ShipRotation)shipRotations.GetValue(random.Next(NumberOfPossibleRotations));

                    return new FourDeckShip(randomType, randomRotation);
                }
                case 3:
                {
                    var randomType = (ShipShape)shipShapes.GetValue(random.Next(NumberOfThreeDeckShipPossibleShapes));
                    var randomRotation = (ShipRotation)shipRotations.GetValue(random.Next(NumberOfThreeDeckShipPossibleRotations));

                    return new ThreeDeckShip(randomType, randomRotation);
                }
                case 2:
                    var randomTwoDeckShipRotation = (ShipRotation)shipRotations.GetValue(random.Next(NumberOfTwoDeckShipPossibleRotations));

                    return new TwoDeckShip(randomTwoDeckShipRotation);
                default:
                    return new OneDeckShip();
            }
        }
    }
}
