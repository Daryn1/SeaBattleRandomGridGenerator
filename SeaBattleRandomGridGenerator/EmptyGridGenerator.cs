using System;
using System.Collections.Generic;
using System.Text;

namespace SeaBattleRandomGridGenerator
{
    public static class EmptyGridGenerator
    {
        public static bool[][] GenerateEmptyGridOfSize(int sizeOfGrid)
        {
            var grid = new bool[sizeOfGrid][];

            for (var i = 0; i < grid.Length; ++i)
            {
                var row = new bool[sizeOfGrid];

                for (var j = 0; j < row.Length; j++)
                {
                    row[j] = false;
                }

                grid[i] = row;
            }

            return grid;
        }
    }
}
