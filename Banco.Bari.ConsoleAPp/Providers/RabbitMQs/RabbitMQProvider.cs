using Banco.Bari.ConsoleApp.Handlers;
using Banco.Bari.ConsoleApp.Models;
using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Banco.Bari.ConsoleApp.Providers
{
    public class RabbitMQProvider : IRabbitMQProvider
    {
        private IBus _bus;
        private readonly IConfiguration _configuration;
        private readonly App _app;
        private readonly IServiceProvider _services;
        public RabbitMQProvider(IConfiguration configuration, IServiceProvider services, App app)
        {
            _configuration = configuration;
            _app = app;
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

            var command = new Message(_app.Name, message);
            _bus.PubSub.Publish(command, topic);
        }

        public void Subscribe<THandler>(string topic) where THandler: IHandler<Message>
        {
            Connect();            

            var handlerInstance = _services.GetService<THandler>();

            _bus.PubSub.Subscribe<Message>(Guid.NewGuid().ToString(), (message) => handlerInstance.Handle(message));
        }
    }
}
