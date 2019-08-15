namespace EcologicalModelApp.Domain.Models
{
    public class Prey : Cell
    {
        protected int TimeToReproduce { get; set; } = DefaultTimeToReproduce;

        public static int DefaultTimeToReproduce { get; set; } = 6;

        public Prey(Ocean ocean) : base(ocean)
        {
            DefaultImage = 'f';
        }

        public override void Process()
        {
            --TimeToReproduce;

            Coordinate coordinate = GetSpecificNeighborCoordinate<Cell>();
            if (!coordinate.Equals(Coordinate))
            {
                if (TimeToReproduce <= 0)
                {
                    TimeToReproduce = DefaultTimeToReproduce;
                    Cell child = Reproduce();
                    Ocean.SwapCell(this, coordinate);
                    Ocean.CreateCell(child);
                }
                else
                {
                    Ocean.SwapCell(this, coordinate);
                }
            }
        }

        public virtual Cell Reproduce()
        {
            return new Prey(Ocean) { Coordinate = Coordinate };
        }
    }
}
