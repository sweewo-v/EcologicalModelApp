using EcologicalModelApp.Domain.Services;

namespace EcologicalModelApp.Console.Services
{
    public class ConsoleWriter : IWriter
    {
        private readonly int _x;
        private readonly int _y;

        public ConsoleWriter(int x, int y)
        {
            _x = x;
            _y = y;

            System.Console.SetCursorPosition(x, y);
        }

        public void Write(string str)
        {
            System.Console.Write(str);
        }

        public void WriteLine(string str)
        {
            System.Console.WriteLine(str);
        }

        public void SetCursorPosition(int x, int y)
        {
            System.Console.SetCursorPosition(_x + x, _y + y);
        }

        public void Clear()
        {
            System.Console.Clear();
        }
    }
}
