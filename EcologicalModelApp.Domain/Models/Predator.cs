using EcologicalModelApp.Domain.Extensions;
using EcologicalModelApp.Domain.Interfaces;

namespace EcologicalModelApp.Domain.Models
{
    public class Predator : Prey
    {
        public int TimeToFeed { get; set; }

        public static int DefaultTimeToFeed { get; set; } = 6;

        public Predator(IMovable ocean) : base(ocean)
        {
            DefaultImage = 'S';
            TimeToFeed = DefaultTimeToFeed;
        }

        public override void Process()
        {
            --TimeToFeed;

            if (TimeToFeed <= 0)
            {
                Ocean.RemoveCellAt(Coordinate);
            }
            else
            {
                Coordinate toCoordinate = this.GetSpecificNeighborCoordinate<Prey>(Ocean);
                if (!toCoordinate.Equals(Coordinate))
                {
                    TimeToFeed = DefaultTimeToFeed;

                    Ocean.RemoveCellAt(toCoordinate);
                    Ocean.MoveCell(this, toCoordinate);
                }
                else
                {
                    base.Process();
                }
            }
        }

        public override Cell Reproduce()
        {
            return new Predator(Ocean) { Coordinate = Coordinate };
        }
    }
}
