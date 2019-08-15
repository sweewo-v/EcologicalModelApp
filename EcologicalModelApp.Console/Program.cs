using EcologicalModelApp.Domain.Models;

namespace EcologicalModelApp.Console
{
    class Program
    {
        static void Main()
        {
            Ocean ocean = new Ocean();
            ocean.Run(10);

            System.Console.ReadKey();
        }
    }
}
