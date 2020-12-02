using Banco.Bari.ConsoleAPp.Commands;
using Banco.Bari.ConsoleAPp.Providers;
using Banco.Bari.ConsoleAPp.Services;
using EasyNetQ.Consumer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Banco.Bari.ConsoleAPp.Handlers
{
    public class MessageHandler : IHandler<MessageCommand>
    {
        private readonly IUserActionService _actionService;

        public MessageHandler(IUserActionService actionService)
        {
            _actionService = actionService;
        }

        public void Handle(MessageCommand message)
        {
            _actionService.PrintMessage(message);   
        }
    }
}
