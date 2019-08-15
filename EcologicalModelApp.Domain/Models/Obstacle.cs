namespace EcologicalModelApp.Domain.Models
{
    class Obstacle : Cell
    {
        public Obstacle(Ocean ocean) : base(ocean)
        {
            DefaultImage = '#';
        }
    }
}
