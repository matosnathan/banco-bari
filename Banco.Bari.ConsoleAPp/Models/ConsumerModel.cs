using Banco.Bari.ConsoleAPp.Commands;
using Banco.Bari.ConsoleAPp.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Banco.Bari.ConsoleAPp.Models
{
    public class ConsumerModel
    {
        public ConsumerModel(string topic, Type handlerType)
        {
            Topic = topic;
            Handler = handlerType;
        }

        public string Topic { get; set; }
        public Type Handler { get; set; }


    }
}
