using System;

namespace SeaBattleRandomGridGenerator
{
    public class GridDrawer
    {
        public void Draw(bool[][] grid)
        {
            Console.Clear();

            for (var y = 0; y < grid.Length; y++)
            {
                for (var x = 0; x < grid.Length; x++)
                {
                    Console.Write(grid[y][x] ? 1 : 0);
                }

                Console.WriteLine();
            }
        }
    }
}
