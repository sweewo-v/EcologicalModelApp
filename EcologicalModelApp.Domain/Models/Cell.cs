using System;

namespace EcologicalModelApp.Domain.Models
{
    public class Cell
    {
        public Coordinate Coordinate { get; set; }

        public char DefaultImage { get; set; } = '-';

        public bool IsDeleted { get; set; } = false;

        protected readonly Ocean Ocean;

        private readonly Random _random = new Random();

        public Cell(Ocean ocean)
        {
            Ocean = ocean;
        }

        public Coordinate GetSpecificNeighborCoordinate<T>()
        where T : Cell
        {
            Cell[] neighbors = new Cell[4];
            int count = 0;

            Cell north = Ocean.GetCell(GetNorthNeighbor());
            Cell south = Ocean.GetCell(GetSouthNeighbor());
            Cell east = Ocean.GetCell(GetEastNeighbor());
            Cell west = Ocean.GetCell(GetWestNeighbor());

            if (north.IsSpecificCell<T>())
            {
                neighbors[count++] = north;
            }
            if (south.IsSpecificCell<T>())
            {
                neighbors[count++] = south;
            }
            if (east.IsSpecificCell<T>())
            {
                neighbors[count++] = east;
            }
            if (west.IsSpecificCell<T>())
            {
                neighbors[count++] = west;
            }

            if (count == 0)
            {
                return Coordinate;
            }

            return neighbors[_random.Next(0, count)].Coordinate;
        }

        public bool IsEmpty()
        {
            return GetType() == typeof(Cell);
        }

        public bool IsSpecificCell<T>()
            where T : Cell
        {
            return GetType() == typeof(T);
        }

        public Coordinate GetNorthNeighbor()
        {
            uint y = Coordinate.Y;
            if (y < 1)
            {
                y = Ocean.NumRows;
            }

            return new Coordinate(Coordinate.X, y - 1);
        }

        public Coordinate GetWestNeighbor()
        {
            uint x = Coordinate.X;
            if (x < 1)
            {
                x = Ocean.NumCols;
            }

            return new Coordinate(x - 1, Coordinate.Y);
        }

        public Coordinate GetSouthNeighbor()
        {
            uint y = Coordinate.Y + 1;
            if (y > Ocean.NumRows - 1)
            {
                y = 0;
            }

            return new Coordinate(Coordinate.X, y);
        }

        public Coordinate GetEastNeighbor()
        {
            uint x = Coordinate.X + 1;
            if (x > Ocean.NumCols - 1)
            {
                x = 0;
            }

            return new Coordinate(x, Coordinate.Y);
        }

        public virtual void Process()
        {
        }
    }
}
