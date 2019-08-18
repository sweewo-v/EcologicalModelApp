using EcologicalModelApp.Domain.Extensions;
using EcologicalModelApp.Domain.Interfaces;

namespace EcologicalModelApp.Domain.Models
{
    public class Prey : Cell
    {
        protected int TimeToReproduce { get; set; } = DefaultTimeToReproduce;

        public static int DefaultTimeToReproduce { get; set; } = 6;

        public Prey(IMovable ocean) : base(ocean)
        {
            DefaultImage = 'f';
        }

        public override void Process()
        {
            --TimeToReproduce;

            Coordinate coordinate = this.GetSpecificNeighborCoordinate<Cell>(Ocean);
            if (!coordinate.Equals(Coordinate))
            {
                if (TimeToReproduce <= 0)
                {
                    TimeToReproduce = DefaultTimeToReproduce;
                    Cell child = Reproduce();

                    Ocean.MoveCell(this, coordinate);
                    Ocean.AddCell(child);
                }
                else
                {
                    Ocean.MoveCell(this, coordinate);
                }
            }
        }

        public virtual Cell Reproduce()
        {
            return new Prey(Ocean) { Coordinate = Coordinate };
        }
    }
}
