using System;
using EcologicalModelApp.Domain.Interfaces;
using EcologicalModelApp.Domain.Models;

namespace EcologicalModelApp.Domain.Extensions
{
    static class OceanInitCellsExtensions
    {
        private static readonly Random Random = new Random();

        public static T[] CreateSpecificCells<T>(this IMatrix matrix, uint count)
            where T : Cell
        {
            T[] cells = new T[count];

            for (int i = 0; i < count; i++)
            {
                Coordinate emptyCellCoordinate = GetEmptyCellCoordinate(matrix);

                T cell = (T)Activator.CreateInstance(typeof(T), matrix);
                cell.Coordinate = emptyCellCoordinate;

                cells[i] = cell;
            }

            return cells;
        }

        private static Coordinate GetEmptyCellCoordinate(IMatrix matrix)
        {
            Coordinate coordinate;

            do
            {
                uint x = (uint)Random.Next(0, (int)matrix.NumCols);
                uint y = (uint)Random.Next(0, (int)matrix.NumRows);

                coordinate = new Coordinate(x, y);
            } while (matrix.GetCellAt(coordinate) != null);

            return coordinate;
        }
    }
}
