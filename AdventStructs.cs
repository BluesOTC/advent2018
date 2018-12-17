using System;

namespace Advent
{
    class Coordinate : IEquatable<Coordinate>
    {
        public int x { get; set; }
        public int y { get; set; }

        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        bool IEquatable<Coordinate>.Equals(Coordinate other)
        {
            return this.x == other.x && this.y == other.y;
        }

        public void Add(Coordinate other)
        {
            this.x += other.x;
            this.y += other.y;
        }

        public Coordinate AddVector(Coordinate other)
        {
            return new Coordinate(this.x + other.x, this.y + other.y);
        }

        public Coordinate GetVector(Coordinate source)
        {
            return new Coordinate(this.x - source.x, this.y - source.y);
        }

        public override string ToString()
        {
            return String.Format("<{0}, {1}>", this.x, this.y);
        }

        public int findManhattanDistance(int x, int y)
        {
            return Math.Abs(this.x - x) + Math.Abs(this.y - y);
        }

        public int findManhattanDistance(Coordinate other)
        {
            return Math.Abs(this.x - other.x) + Math.Abs(this.y - other.y);
        }
    }
}
