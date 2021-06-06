namespace SeaBattleRandomGridGenerator.Ships
{
    public class ThreeDeckShip : Ship
    {
        public ThreeDeckShip(ShipShape shape, ShipRotation shipRotation)
        {
            Size = 3;
            Shape = shape;
            Rotation = shipRotation;
        }
    }
}
