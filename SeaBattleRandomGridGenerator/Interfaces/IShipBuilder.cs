using System.Collections.Generic;
using SeaBattleRandomGridGenerator.Ships;

namespace SeaBattleRandomGridGenerator.Interfaces
{
    public interface IShipBuilder
    {
        List<Cell> Build(Ship ship);
    }
}
