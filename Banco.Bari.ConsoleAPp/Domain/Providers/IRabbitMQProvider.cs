using System;

namespace Banco.Bari.ConsoleApp.Providers
{
    public interface IRabbitMQProvider
    {
        void Publish(Message message, string topic);

        void Subscribe(string topic, Action<Message> onMessage);

    }
}
