using System;
using System.Collections.Generic;
using System.Text;

namespace Banco.Bari.ConsoleAPp.Handlers
{
    public interface IHandler<MessageCommand>
    {
        void Handle(MessageCommand message);
    }
}
