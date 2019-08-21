using System;
using EcologicalModelApp.Domain.Interfaces;
using EcologicalModelApp.Domain.Models;

namespace EcologicalModelApp.Domain.Extensions
{
    static class OceanInitCellsExtensions
    {
        private static readonly Random Random = new Random();

        public static T[] CreateSpecificCells<T>(this IContainer container, uint count)
            where T : Cell
        {
            T[] cells = new T[count];

            for (int i = 0; i < count; i++)
            {
                Coordinate emptyCellCoordinate = GetEmptyCellCoordinate(container);

                T cell = (T)Activator.CreateInstance(typeof(T), container);
                cell.Coordinate = emptyCellCoordinate;

                cells[i] = cell;
            }

            return cells;
        }

        private static Coordinate GetEmptyCellCoordinate(IContainer container)
        {
            Coordinate coordinate;

            do
            {
                uint x = (uint)Random.Next(0, (int)container.NumCols);
                uint y = (uint)Random.Next(0, (int)container.NumRows);

                coordinate = new Coordinate(x, y);
            } while (container.GetCellAt(coordinate) != null);

            return coordinate;
        }
    }
}
