namespace SeaBattleRandomGridGenerator.Ships
{
    public abstract class Ship
    {
        public int Size { get; set; }

        public ShipShape Shape { get; set; }

        public ShipRotation Rotation { get; set; }
    }
}
