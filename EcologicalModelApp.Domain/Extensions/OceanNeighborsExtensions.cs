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
            (this Cell cell, IMatrix matrix) where T : Cell
        {
            List<Coordinate> neighbors;
            List<Coordinate> neighborsCoordinates = new List<Coordinate>
            {
                GetNorthNeighbor(cell.Coordinate, matrix),
                GetSouthNeighbor(cell.Coordinate,matrix),
                GetEastNeighbor(cell.Coordinate, matrix),
                GetWestNeighbor(cell.Coordinate, matrix)
            };

            if (typeof(T) == typeof(Cell))
            {
                neighbors = GetEmptyNeighborsCoordinates(neighborsCoordinates, matrix);
            }
            else
            {
                neighbors = GetSpecificNeighborsCoordinates<T>(neighborsCoordinates, matrix);
            }

            if (neighbors.Count == 0)
            {
                return cell.Coordinate;
            }

            return neighbors[Random.Next(0, neighbors.Count)];
        }

        private static List<Coordinate> GetSpecificNeighborsCoordinates<T>
        (List<Coordinate> values, IMatrix matrix) where T : Cell
        {
            List<Coordinate> coordinates = new List<Coordinate>();

            foreach (var value in values)
            {
                Cell cell = matrix.GetCellAt(value);

                if (cell != null && cell.IsSpecificCell<T>())
                {
                    coordinates.Add(value);
                }
            }

            return coordinates;
        }

        private static List<Coordinate> GetEmptyNeighborsCoordinates
        (List<Coordinate> values, IMatrix matrix)
        {
            List<Coordinate> coordinates = new List<Coordinate>();

            foreach (var value in values)
            {
                Cell cell = matrix.GetCellAt(value);

                if (cell == null)
                {
                    coordinates.Add(value);
                }
            }

            return coordinates;
        }

        public static Coordinate GetNorthNeighbor(Coordinate coordinate, IMatrix matrix)
        {
            uint y = coordinate.Y;
            if (y < 1)
            {
                y = matrix.NumRows;
            }

            return new Coordinate(coordinate.X, y - 1);
        }

        public static Coordinate GetWestNeighbor(Coordinate coordinate, IMatrix matrix)
        {
            uint x = coordinate.X;
            if (x < 1)
            {
                x = matrix.NumCols;
            }

            return new Coordinate(x - 1, coordinate.Y);
        }

        public static Coordinate GetSouthNeighbor(Coordinate coordinate, IMatrix matrix)
        {
            uint y = coordinate.Y + 1;
            if (y > matrix.NumRows - 1)
            {
                y = 0;
            }

            return new Coordinate(coordinate.X, y);
        }

        public static Coordinate GetEastNeighbor(Coordinate coordinate, IMatrix matrix)
        {
            uint x = coordinate.X + 1;
            if (x > matrix.NumCols - 1)
            {
                x = 0;
            }

            return new Coordinate(x, coordinate.Y);
        }
    }
}
