using Banco.Bari.ConsoleAPp.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Banco.Bari.ConsoleAPp.Services
{
    public class UserActionService : IUserActionService
    {
        public ConsoleKeyInfo Menu()
        {
            Console.WriteLine();
            Console.WriteLine("Como você deseja configurar este serviço?");
            Console.WriteLine();
            Console.WriteLine("1- Publisher");
            Console.WriteLine("2- Consumer");
            Console.WriteLine();

            return Console.ReadKey();
        }

        public string SendMessage()
        {
            Console.WriteLine("Digite a mensagem que deseja enviar ou 0 para sair");
            Console.WriteLine();

            return Console.ReadLine();
        }

        public void PrintMessage(MessageCommand message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("___________");
            Console.WriteLine($"Recived message from {message.Origin}");
            Console.WriteLine($"At {message.Time.ToString("dd\\.hh\\:mm\\:ss")}");
            Console.WriteLine($"Message {message.Id}");
            Console.WriteLine(message.Message);
            Console.WriteLine("___________");
            Console.ResetColor();
        }
    }
}
