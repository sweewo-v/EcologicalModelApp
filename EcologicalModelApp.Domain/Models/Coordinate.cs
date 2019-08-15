namespace EcologicalModelApp.Domain.Models
{
    public class Coordinate
    {
        public uint X { get; }

        public uint Y { get; }

        public Coordinate(uint x, uint y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Coordinate;

            if (item is null)
            {
                return false;
            }

            return X.Equals(item.X)
                   && Y.Equals(item.Y);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }
    }
}
