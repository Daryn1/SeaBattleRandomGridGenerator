using SeaBattleRandomGridGenerator.Ships;

namespace SeaBattleRandomGridGenerator.Interfaces
{
    public interface IShipGenerator
    {
        Ship GenerateShipOfSize(int shipSize);
    }
}
