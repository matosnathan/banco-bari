namespace Banco.Bari.ConsoleApp.Services
{
    public interface IWritterService
    {
        string Menu();
        string ReadResponse();
        string Question(string questionMessage);
        void PrintMessage(string message);
        void PrintMessage(Message message);
    }
}
