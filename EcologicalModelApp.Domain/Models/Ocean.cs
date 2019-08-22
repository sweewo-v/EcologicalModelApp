using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using EcologicalModelApp.Domain.Extensions;
using EcologicalModelApp.Domain.Interfaces;
using EcologicalModelApp.Domain.Services;

namespace EcologicalModelApp.Domain.Models
{
    public class Ocean : IRunnable, IMovable
    {
        public uint NumRows { get; } = 25;

        public uint NumCols { get; } = 50;

        public uint NumPrey { get; } = 150;

        public uint NumPredator { get; } = 100;

        public uint NumObstacle { get; } = 75;

        private readonly List<Cell> _cells;

        private readonly IWriter _writer;

        public Ocean(IWriter writer)
        {
            _writer = writer;
            _cells = new List<Cell>();

            InitCells();
        }

        public Cell GetCellAt(Coordinate coordinate)
        {
            return _cells.FirstOrDefault(c => !c.IsDeleted
                                              && c.Coordinate.Equals(coordinate));
        }

        private void Clear()
        {
            foreach (var item in _cells.Where(c => c.IsDeleted).ToList())
            {
                _cells.Remove(item);
            }
        }

        public void AddCell(Cell cell)
        {
            _cells.Add(cell);
        }

        public void MoveCell(Cell cell, Coordinate coordinate)
        {
            cell.Coordinate = coordinate;
        }

        public void RemoveCellAt(Coordinate coordinate)
        {
            Cell cell = GetCellAt(coordinate);
            cell.IsDeleted = true;
        }

        private void InitCells()
        {
            _cells.AddRange(this.CreateSpecificCells<Obstacle>(NumObstacle));
            _cells.AddRange(this.CreateSpecificCells<Prey>(NumPrey));
            _cells.AddRange(this.CreateSpecificCells<Predator>(NumPredator));
        }

        public void Run(uint count)
        {
            for (uint i = 0; i <= count
               && _cells.Count(c => c.IsSpecificCell<Prey>()) > 0
               && _cells.Count(c => c.IsSpecificCell<Predator>()) > 0; i++)
            {
                _writer.Clear();

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
        private void Display()
        {
            foreach (var val in _cells)
            {
                _writer.SetCursorPosition(
                    (int)val.Coordinate.X,
                    (int)val.Coordinate.Y);
                _writer.Write(val.DefaultImage.ToString());
            }
        }

        private void DisplayStats(uint i)
        {
            _writer.SetCursorPosition(0, (int)NumRows + 1);

            _writer.Write($"Obstacle: {_cells.Count(c => c.IsSpecificCell<Obstacle>())}. ");
            _writer.Write($"Prey: {_cells.Count(c => c.IsSpecificCell<Prey>())}. ");
            _writer.Write($"Predator: {_cells.Count(c => c.IsSpecificCell<Predator>())}. ");
            _writer.WriteLine($"Iteration: {i}");
        }
    }
}
