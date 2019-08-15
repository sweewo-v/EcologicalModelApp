using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace EcologicalModelApp.Domain.Models
{
    public class Ocean
    {
        public uint NumRows { get; } = 25;

        public uint NumCols { get; } = 70;

        public uint NumPrey { get; set; } = 150;

        public uint NumPredator { get; } = 100;

        public uint NumObstacle { get; } = 75;

        private readonly IList<Cell> _cells;

        private static Random _random = new Random();

        public Ocean()
        {
            _cells = new List<Cell>();

            InitCells();
        }

        public Cell GetCell(Coordinate coordinate)
        {
            return _cells.FirstOrDefault(c => c.Coordinate.Equals(coordinate) && !c.IsDeleted);
        }

        public void CreateCell(Cell cell)
        {
            Cell temp = GetCell(cell.Coordinate);
            temp.IsDeleted = true;

            _cells.Add(cell);
        }

        private void Clear()
        {
            foreach (var item in _cells.Where(c => c.IsDeleted).ToList())
            {
                _cells.Remove(item);
            }
        }

        public void SwapCell(Cell cell, Coordinate coordinate)
        {
            Cell temp = GetCell(coordinate);

            temp.Coordinate = cell.Coordinate;
            cell.Coordinate = coordinate;
        }

        private void InitCells()
        {
            AddEmptyCells();

            AddCells<Obstacle>(NumObstacle);
            AddCells<Predator>(NumPredator);
            AddCells<Prey>(NumPrey);
        }

        private void AddCells<T>(uint count)
            where T : Cell
        {
            for (int i = 0; i < count; i++)
            {
                Coordinate emptyCellCoordinate = GetEmptyCellCoordinate();

                Cell emptyCell = GetCell(emptyCellCoordinate);
                if (emptyCell != null)
                {
                    _cells.Remove(emptyCell);
                }

                T cell = (T)Activator.CreateInstance(typeof(T), this);
                cell.Coordinate = emptyCellCoordinate;

                _cells.Add(cell);
            }
        }

        private Coordinate GetEmptyCellCoordinate()
        {
            Coordinate coordinate;

            do
            {
                uint x = (uint)_random.Next(0, (int)NumCols);
                uint y = (uint)_random.Next(0, (int)NumRows);

                coordinate = new Coordinate(x, y);
            } while (!GetCell(coordinate).IsEmpty());

            return coordinate;
        }

        private void AddEmptyCells()
        {
            for (uint i = 0; i < NumRows; i++)
            {
                for (uint j = 0; j < NumCols; j++)
                {
                    _cells.Add(new Cell(this)
                    {
                        Coordinate = new Coordinate(j, i)
                    });
                }
            }
        }

        public void Display()
        {
            foreach (var val in _cells)
            {
                Console.SetCursorPosition(
                    (int)val.Coordinate.X,
                    (int)val.Coordinate.Y);
                Console.Write(val.DefaultImage);
            }
        }

        public void Run(uint count)
        {
            for (uint i = 0; i <= count
               && _cells.Count(c => c.IsSpecificCell<Prey>()) > 0
               && _cells.Count(c => c.IsSpecificCell<Predator>()) > 0; i++)
            {
                Display();
                DisplayStats(i);

                Thread.Sleep(500);

                foreach (var cell in _cells.ToList())
                {
                    if (!cell.IsDeleted)
                    {
                        cell.Process();
                    }
                }

                Clear();
            }
        }

        private void DisplayStats(uint i)
        {
            Console.SetCursorPosition(0, (int)NumRows + 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, (int)NumRows + 1);

            Console.Write($"Obstacle: {_cells.Count(c => c.IsSpecificCell<Obstacle>())}. ");
            Console.Write($"Prey: {_cells.Count(c => c.IsSpecificCell<Prey>())}. ");
            Console.Write($"Predator: {_cells.Count(c => c.IsSpecificCell<Predator>())}. ");
            Console.WriteLine($"Iteration: {i}");
        }
    }
}
