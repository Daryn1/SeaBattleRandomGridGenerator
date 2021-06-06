namespace SeaBattleRandomGridGenerator.Ships
{
    public class TwoDeckShip : Ship
    {
        public TwoDeckShip(ShipRotation shipRotation)
        {
            Size = 2;
            Rotation = shipRotation;
            Shape = ShipShape.Line;
        }
    }
}
