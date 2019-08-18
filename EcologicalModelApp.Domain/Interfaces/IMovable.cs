using EcologicalModelApp.Domain.Models;

namespace EcologicalModelApp.Domain.Interfaces
{
    public interface IMovable : IMatrix
    {
        void RemoveCellAt(Coordinate coordinate);

        void MoveCell(Cell cell, Coordinate coordinate);

        void AddCell(Cell cell);
    }
}
