using EcologicalModelApp.Domain.Models;

namespace EcologicalModelApp.Domain.Interfaces
{
    public interface IMovable : IContainer
    {
        void RemoveCellAt(Coordinate coordinate);

        void MoveCell(Cell cell, Coordinate coordinate);

        void AddCell(Cell cell);
    }
}
