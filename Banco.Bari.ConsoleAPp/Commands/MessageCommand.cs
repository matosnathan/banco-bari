using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client.Events;
namespace Banco.Bari.ConsoleAPp.Commands
{
    public class MessageCommand
    {
        public MessageCommand(string origin, string message)
        {
            Id = Guid.NewGuid();
            Origin = origin;
            Time = DateTime.Now.TimeOfDay;
            Message = message;
        }
        protected MessageCommand() { }

        public Guid Id { get; private set; }
        public string Origin { get; private set; }
        public TimeSpan Time { get; private set; }
        public string Message { get; private set; }
    }
}
