using EcologicalModelApp.Console.Services;
using EcologicalModelApp.Domain.Interfaces;
using EcologicalModelApp.Domain.Models;

namespace EcologicalModelApp.Console
{
    class Program
    {
        static void Main()
        {
            IRunnable ocean1 = new Ocean(new ConsoleWriter(0, 0));
            ocean1.Run(10);
            
            System.Console.ReadKey();
        }
    }
}
