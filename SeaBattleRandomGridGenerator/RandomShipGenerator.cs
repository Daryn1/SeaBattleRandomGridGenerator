using System;
using System.Collections.Generic;
using SeaBattleRandomGridGenerator.Interfaces;
using SeaBattleRandomGridGenerator.Ships;

namespace SeaBattleRandomGridGenerator
{
    public class RandomShipGenerator : IShipGenerator
    {
        private readonly IRandomGenerator randomGenerator;

        public RandomShipGenerator(IRandomGenerator randomGenerator)
        {
            this.randomGenerator = randomGenerator;
        }

        public Ship GenerateShipOfSize(int shipSize)
        {
            return shipSize switch
            {
                4 => GenerateRandomFourDeckShip(),
                3 => GenerateRandomThreeDeckShip(),
                2 => GenerateRandomTwoDeckShip(),
                _ => new OneDeckShip()
            };
        }

        private Ship GenerateRandomFourDeckShip()
        {
            var shipShapes = Enum.GetValues(typeof(ShipShape));
            var shipRotations = Enum.GetValues(typeof(ShipRotation));
            var randomType = (ShipShape)randomGenerator.GetRandomElementFromArrayAsInt(shipShapes);

            // Ограничиваем число возможных поворотов корабля, для того, чтобы один и тот же корабль
            // не мог быть сгенерирован двумя разными комбинациями формы и поворота, в результате чего
            // генерация кораблей будет производится равновероятно.
            Array possibleRotations;
            switch (randomType)
            {
                case ShipShape.Line:
                case ShipShape.Zigzag:
                case ShipShape.ZigzagMirrored:
                    possibleRotations = new[] { ShipRotation._0, ShipRotation._90 };
                    break;
                case ShipShape.LShaped:
                case ShipShape.LShapedMirrored:
                    possibleRotations = shipRotations;
                    break;
                case ShipShape.Square:
                    possibleRotations = new[] { ShipRotation._0 };
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(randomType));
            }

            var randomRotation = (ShipRotation)randomGenerator.GetRandomElementFromArrayAsInt(possibleRotations);

            return new FourDeckShip(randomType, randomRotation);
        }

        private Ship GenerateRandomThreeDeckShip()
        {
            var randomType = (ShipShape)randomGenerator.GetRandomElementFromArrayAsInt(new[] { ShipShape.Line, ShipShape.LShaped });
            var randomRotation = (ShipRotation)randomGenerator.GetRandomElementFromArrayAsInt(new[] { ShipRotation._0, ShipRotation._90 });

            return new ThreeDeckShip(randomType, randomRotation);
        }

        private Ship GenerateRandomTwoDeckShip()
        {
            var randomRotation = (ShipRotation)randomGenerator.GetRandomElementFromArrayAsInt(new[] { ShipRotation._0, ShipRotation._90 });

            return new TwoDeckShip(randomRotation);
        }
    }
}
