using SeaBattleRandomGridGenerator.Ships;

namespace SeaBattleRandomGridGenerator.Interfaces
{
    public interface IShipPlacer
    {
        void PlaceRandomly(Ship ship, bool[][] grid);
    }
}