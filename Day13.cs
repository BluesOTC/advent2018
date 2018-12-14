using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent
{
    class Cart : IComparable<Cart>
    {
        public int x { get; set; }
        public int y { get; set; }
        DirectionType heading { get; set; }
        int nextIntersection { get; set; }
        public char currTrack;

        public Cart(int x, int y, DirectionType heading)
        {
            this.x = x;
            this.y = y;
            this.heading = heading;
            nextIntersection = 1;
            switch(heading)
            {
                case DirectionType.NORTH:
                case DirectionType.SOUTH:
                    currTrack = '|';
                    break;
                case DirectionType.EAST:
                case DirectionType.WEST:
                    currTrack = '-';
                    break;
            }
        }

        public bool move(ref List<char[]> grid) //change cart locations to proper symbol
        {
            grid[y][x] = currTrack;
            advance();
            currTrack = grid[y][x];
            if (currTrack == 'c')
                return true;
            else if (currTrack == '+')
                doIntersection();
            else
            {
                switch (heading)
                {
                    case DirectionType.NORTH:
                        if (currTrack == '/')
                            heading = DirectionType.EAST;
                        else if (currTrack == '\\')
                            heading = DirectionType.WEST;
                        break;
                    case DirectionType.EAST:
                        if (currTrack == '/')
                            heading = DirectionType.NORTH;
                        else if (currTrack == '\\')
                            heading = DirectionType.SOUTH;
                        break;
                    case DirectionType.SOUTH:
                        if (currTrack == '/')
                            heading = DirectionType.WEST;
                        else if (currTrack == '\\')
                            heading = DirectionType.EAST;
                        break;
                    case DirectionType.WEST:
                        if (currTrack == '/')
                            heading = DirectionType.SOUTH;
                        else if (currTrack == '\\')
                            heading = DirectionType.NORTH;
                        break;
                }
            }
            grid[y][x] = 'c';
            return false;
        }

        void advance()
        {
            switch (heading)
            {
                case DirectionType.NORTH:
                    y = y - 1;
                    break;
                case DirectionType.EAST:
                    x = x + 1;
                    break;
                case DirectionType.SOUTH:
                    y = y + 1;
                    break;
                case DirectionType.WEST:
                    x = x - 1;
                    break;
            }
        }

        void doIntersection()
        {
            switch(nextIntersection)
            {
                case 1:
                    if (heading == DirectionType.NORTH)
                        heading = DirectionType.WEST;
                    else
                        heading = heading - 1;
                    break;
                case 3:
                    if (heading == DirectionType.WEST)
                        heading = DirectionType.NORTH;
                    else
                        heading = heading + 1;
                    nextIntersection = 0;
                    break;
            }
            nextIntersection++;
        }

        int IComparable<Cart>.CompareTo(Cart other)
        {
            if (this.y != other.y)
                return this.y - other.y;
            return this.x - other.x;
        }
    }

    public enum DirectionType
    {
        NORTH,
        EAST,
        SOUTH,
        WEST,
        NUM_TYPES
    }

    class Day13
    {
        public static void Run(List<string> input)
        {
            List<char[]> grid = new List<char[]>();
            List<char[]> origGrid = new List<char[]>();
            List<Cart> carts = new List<Cart>();

            for (int y = 0; y < input.Count; y++)
            {
                grid.Add(input[y].ToCharArray());
                origGrid.Add(input[y].ToCharArray());
                for (int x = 0; x < input[0].Length; x++)
                {
                    if (grid[y][x] == '<')
                    {
                        carts.Add(new Cart(x, y, DirectionType.WEST));
                        grid[y][x] = 'c';
                        origGrid[y][x] = '-';
                    }
                    else if (grid[y][x] == '>')
                    {
                        carts.Add(new Cart(x, y, DirectionType.EAST));
                        grid[y][x] = 'c';
                        origGrid[y][x] = '-';
                    }
                    else if (grid[y][x] == 'v')
                    {
                        carts.Add(new Cart(x, y, DirectionType.SOUTH));
                        grid[y][x] = 'c';
                        origGrid[y][x] = '|';
                    }
                    else if (grid[y][x] == '^')
                    {
                        carts.Add(new Cart(x, y, DirectionType.NORTH));
                        grid[y][x] = 'c';
                        origGrid[y][x] = '|';
                    }
                }
            }

            bool hasCrashed = false;
            while (carts.Count > 1)
            {
                List<Cart> next = new List<Cart>(carts);
                carts.Sort();
                foreach (Cart c in carts)
                {
                    if (!next.Contains(c))
                        continue;
                    if (hasCrashed = c.move(ref grid))
                    {
                        int removed = next.RemoveAll(x => x.x == c.x && x.y == c.y);
                        grid[c.y][c.x] = origGrid[c.y][c.x];
                        //Console.WriteLine("Crashed! " + c.x + "," + c.y);
                        //break;
                    }
                }
                carts = next;
            }
            Console.WriteLine("One cart left! " + carts[0].x + "," + carts[0].y);
        }
    }
}
