using System;
using System.Collections.Generic;
using System.Text;
using SeaBattleRandomGridGenerator.Interfaces;
using SeaBattleRandomGridGenerator.Ships;

namespace SeaBattleRandomGridGenerator
{
    public class RandomGenerator : IRandomGenerator
    {
        private readonly Random random = new Random();

        public int GetRandomElementFromArrayAsInt(Array array)
        {
            return (int)array.GetValue(random.Next(array.Length)); ;
        }

        public int GetRandomNumberFromRange(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }
    }
}
