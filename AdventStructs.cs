using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override string ToString()
        {
            return String.Format("<{0}, {1}>", this.x, this.y);
        }
    }
}
