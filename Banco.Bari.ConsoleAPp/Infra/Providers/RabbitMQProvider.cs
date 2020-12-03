using Banco.Bari.ConsoleApp.Domain.Models;
using EasyNetQ;
using System;

namespace Banco.Bari.ConsoleApp.Providers
{
    public class RabbitMQProvider : IRabbitMQProvider
    {
        private IBus _bus;
        private readonly MQConnection _connection;
        public RabbitMQProvider(MQConnection connection)
        {
            _connection = connection;
        }
        private void Connect()
        {
            if (_bus == null || !_bus.Advanced.IsConnected)
                _bus = RabbitHutch.CreateBus(_connection.ConnectionString);
        }        

        public void Publish(Message message, string topic)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            Connect();

            _bus.PubSub.Publish(message, topic);
        }

        public void Subscribe(string topic, Action<Message> onMessage)
        {
            if (string.IsNullOrEmpty(topic))
                throw new ArgumentNullException(nameof(topic));

            if (onMessage == null)
                throw new ArgumentNullException(nameof(onMessage));

            Connect();

            _bus.PubSub.Subscribe<Message>(Guid.NewGuid().ToString(), onMessage, config=> config.WithTopic(topic));
        }
    }
}
