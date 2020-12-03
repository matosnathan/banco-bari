using Banco.Bari.ConsoleApp.Handlers;
using Banco.Bari.ConsoleApp.Models;
using Banco.Bari.ConsoleApp.Providers;
using System;

namespace Banco.Bari.ConsoleApp.Services
{
    public class MessagingFacade : IMessagingFacade
    {
        private readonly IRabbitMQProvider _mqProvider;
        private readonly App _app;
        private readonly MessageHandler _messageHandler;
        public MessagingFacade(IRabbitMQProvider mqProvider, App app, MessageHandler messageHandler)
        {
            _mqProvider = mqProvider;
            _app = app;
            _messageHandler = messageHandler;
        }

        public void Publish(string message, string topic)
        {
            if (string.IsNullOrEmpty(message) || string.IsNullOrEmpty(topic))
                throw new ArgumentNullException();

            _mqProvider.Publish(new Message(_app.Name, message), topic);
        }

        public void Subscribe(string topic)
        {
            if (string.IsNullOrEmpty(topic))
                throw new ArgumentNullException(nameof(topic));

            _mqProvider.Subscribe(topic, (message) => _messageHandler.Handle(message));
        }
    }
}
