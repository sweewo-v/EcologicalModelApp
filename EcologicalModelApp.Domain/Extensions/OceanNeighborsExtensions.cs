using System;
using System.Collections.Generic;
using EcologicalModelApp.Domain.Interfaces;
using EcologicalModelApp.Domain.Models;

namespace EcologicalModelApp.Domain.Extensions
{
    static class OceanNeighborsExtensions
    {
        private static readonly Random Random = new Random();

        public static Coordinate GetSpecificNeighborCoordinate<T>
            (this IContainer container, Cell cell) where T : Cell
        {
            List<Coordinate> neighbors;
            List<Coordinate> neighborsCoordinates = new List<Coordinate>
            {
                GetNorthNeighbor(cell.Coordinate, container),
                GetSouthNeighbor(cell.Coordinate,container),
                GetEastNeighbor(cell.Coordinate, container),
                GetWestNeighbor(cell.Coordinate, container)
            };

            if (typeof(T) == typeof(Cell))
            {
                neighbors = GetEmptyNeighborsCoordinates(neighborsCoordinates, container);
            }
            else
            {
                neighbors = GetSpecificNeighborsCoordinates<T>(neighborsCoordinates, container);
            }

            if (neighbors.Count == 0)
            {
                return cell.Coordinate;
            }

            return neighbors[Random.Next(0, neighbors.Count)];
        }

        private static List<Coordinate> GetSpecificNeighborsCoordinates<T>
        (List<Coordinate> values, IContainer container) where T : Cell
        {
            List<Coordinate> coordinates = new List<Coordinate>();

            foreach (var value in values)
            {
                Cell cell = container.GetCellAt(value);

                if (cell != null && cell.IsSpecificCell<T>())
                {
                    coordinates.Add(value);
                }
            }

            return coordinates;
        }

        private static List<Coordinate> GetEmptyNeighborsCoordinates
        (List<Coordinate> values, IContainer container)
        {
            List<Coordinate> coordinates = new List<Coordinate>();

            foreach (var value in values)
            {
                Cell cell = container.GetCellAt(value);

                if (cell == null)
                {
                    coordinates.Add(value);
                }
            }

            return coordinates;
        }

        private static Coordinate GetNorthNeighbor(Coordinate coordinate, IContainer container)
        {
            uint y = coordinate.Y;
            if (y < 1)
            {
                y = container.NumRows;
            }

            return new Coordinate(coordinate.X, y - 1);
        }

        private static Coordinate GetWestNeighbor(Coordinate coordinate, IContainer container)
        {
            uint x = coordinate.X;
            if (x < 1)
            {
                x = container.NumCols;
            }

            return new Coordinate(x - 1, coordinate.Y);
        }

        private static Coordinate GetSouthNeighbor(Coordinate coordinate, IContainer container)
        {
            uint y = coordinate.Y + 1;
            if (y > container.NumRows - 1)
            {
                y = 0;
            }

            return new Coordinate(coordinate.X, y);
        }

        private static Coordinate GetEastNeighbor(Coordinate coordinate, IContainer container)
        {
            uint x = coordinate.X + 1;
            if (x > container.NumCols - 1)
            {
                x = 0;
            }

            return new Coordinate(x, coordinate.Y);
        }
    }
}
