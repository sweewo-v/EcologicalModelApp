using EcologicalModelApp.Domain.Interfaces;

namespace EcologicalModelApp.Domain.Models
{
    public abstract class Cell
    {
        public Coordinate Coordinate { get; set; }

        public char DefaultImage { get; set; }

        public bool IsDeleted { get; set; } = false;

        protected readonly IMovable Ocean;

        protected Cell(IMovable ocean)
        {
            Ocean = ocean;
        }

        public bool IsSpecificCell<T>() where T : Cell
        {
            return GetType() == typeof(T);
        }

        public abstract void Process();
    }
}
