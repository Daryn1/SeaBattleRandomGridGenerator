using System;

namespace SeaBattleRandomGridGenerator.Interfaces
{
    public interface IRandomGenerator
    {
        int GetRandomElementFromArrayAsInt(Array array);

        int GetRandomNumberFromRange(int minValue, int maxValue);
    }
}