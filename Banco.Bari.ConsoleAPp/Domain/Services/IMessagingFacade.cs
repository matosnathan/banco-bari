using Banco.Bari.ConsoleApp.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Banco.Bari.ConsoleApp.Services
{
    public interface IMessagingFacade
    {
        void Publish(string message, string topic);
        void Subscribe(string topic);

    }
}
