namespace EcologicalModelApp.Domain.Models
{
    public class Predator : Prey
    {
        public int TimeToFeed { get; set; }

        public static int DefaultTimeToFeed { get; set; } = 6;

        public Predator(Ocean ocean) : base(ocean)
        {
            DefaultImage = 'S';
            TimeToFeed = DefaultTimeToFeed;
        }

        public override void Process()
        {
            --TimeToFeed;

            if (TimeToFeed <= 0)
            {
                Ocean.CreateCell(new Cell(Ocean) { Coordinate = Coordinate });
            }
            else
            {
                Coordinate toCoordinate = GetSpecificNeighborCoordinate<Prey>();
                if (!toCoordinate.Equals(Coordinate))
                {
                    TimeToFeed = DefaultTimeToFeed;
                    Ocean.CreateCell(new Cell(Ocean) { Coordinate = toCoordinate });
                    Ocean.SwapCell(this, toCoordinate);
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
