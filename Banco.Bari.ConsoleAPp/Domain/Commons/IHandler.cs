namespace Banco.Bari.ConsoleApp.Handlers
{
    public interface IHandler<MessageCommand>
    {
        void Handle(MessageCommand message);
    }
}
