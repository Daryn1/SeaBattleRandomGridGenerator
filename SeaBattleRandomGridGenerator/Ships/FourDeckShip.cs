namespace SeaBattleRandomGridGenerator.Ships
{
    public class FourDeckShip : Ship
    {
        public FourDeckShip(ShipShape shape, ShipRotation shipRotation)
        {
            Size = 4;
            Shape = shape;
            Rotation = shipRotation;
        }
    }
}
