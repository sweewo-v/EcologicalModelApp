namespace EcologicalModelApp.Domain.Services
{
    public interface IWriter
    {
        void Write(string str);

        void WriteLine(string str);

        void SetCursorPosition(int x, int y);

        void Clear();
    }
}
