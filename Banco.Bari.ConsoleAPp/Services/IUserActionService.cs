using Banco.Bari.ConsoleAPp.Commands;
using System;

namespace Banco.Bari.ConsoleAPp.Services
{
    public interface IUserActionService
    {
        ConsoleKeyInfo Menu();
        string SendMessage();
        void PrintMessage(MessageCommand message);
    }
}
