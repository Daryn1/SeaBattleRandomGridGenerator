using System;

namespace SeaBattleRandomGridGenerator
{
    public class Cell
    {
        public int Y { get; set; }

        public int X { get; set; }

        public Cell(int y, int x)
        {
            Y = y;
            X = x;
        }

        protected bool Equals(Cell other)
        {
            return Y == other.Y && X == other.X;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Cell other))
            {
                return false;
            }

            return Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Y, X);
        }

        public override string ToString()
        {
            return $"({Y}, {X})";
        }
    }
}
