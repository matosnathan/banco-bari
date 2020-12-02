using Banco.Bari.ConsoleAPp.Commands;
using Banco.Bari.ConsoleAPp.Handlers;
using Banco.Bari.ConsoleAPp.Models.Settings;
using EasyNetQ;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Banco.Bari.ConsoleAPp.Providers
{
    public class RabbitMQProvider : IRabbitMQProvider
    {
        private IBus _bus;
        private readonly IConfiguration _configuration;
        private readonly App _app;
        private readonly Consumers _consumers;
        private readonly IServiceProvider _services;
        public RabbitMQProvider(IConfiguration configuration, IServiceProvider services, App app, Consumers consumers)
        {
            _configuration = configuration;
            _app = app;
            _consumers = consumers;
            _services = services;
        }
        private void Connect()
        {
            if (_bus == null || !_bus.Advanced.IsConnected)
                _bus = RabbitHutch.CreateBus(_configuration.GetSection("RabbitMQ:Host").Value);
        }        

        public void Publish(string message, string topic)
        {
            Connect();

            var command = new MessageCommand(_app.Name, message);
            _bus.PubSub.Publish(command, topic);
        }

        public void Subscribe(string topic)
        {
            Connect();

            var handler = _consumers.Handlers.FirstOrDefault(x => x.Topic == topic);
            if (handler == null)
                return;

            var handlerInstance = (IHandler<MessageCommand>)_services.GetService(handler.Handler);

            _bus.PubSub.Subscribe<MessageCommand>(Guid.NewGuid().ToString(), (message) => handlerInstance.Handle(message));
        }
    }
}
