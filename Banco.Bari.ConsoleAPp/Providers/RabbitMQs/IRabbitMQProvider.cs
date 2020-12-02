using Banco.Bari.ConsoleApp.Handlers;

namespace Banco.Bari.ConsoleApp.Providers
{
    public interface IRabbitMQProvider
    {
        void Publish(string message, string topic);

        void Subscribe<THandler>(string topic) where THandler : IHandler<Message>;

    }
}
