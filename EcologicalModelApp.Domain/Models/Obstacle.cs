using EcologicalModelApp.Domain.Interfaces;

namespace EcologicalModelApp.Domain.Models
{
    class Obstacle : Cell
    {
        public Obstacle(IMovable ocean) : base(ocean)
        {
            DefaultImage = '#';
        }

        public override void Process()
        {
        }
    }
}
