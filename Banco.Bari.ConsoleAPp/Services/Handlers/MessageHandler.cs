using Banco.Bari.ConsoleApp.Services;

namespace Banco.Bari.ConsoleApp.Handlers
{
    public class MessageHandler : IHandler<Message>
    {
        private readonly IWritterService _actionService;

        public MessageHandler(IWritterService actionService)
        {
            _actionService = actionService;
        }

        public void Handle(Message message)
        {
            _actionService.PrintMessage(message);   
        }
    }
}
