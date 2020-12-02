using System;

namespace Banco.Bari.ConsoleApp.Services
{
    public class WritterService : IWritterService
    {
        public string Menu()
        {
            Console.WriteLine();
            Console.WriteLine("Como você deseja configurar este serviço?");
            Console.WriteLine();
            Console.WriteLine("1- Publisher");
            Console.WriteLine("2- Automatic Publisher");
            Console.WriteLine("3- Consumer");
            Console.WriteLine();

            return Console.ReadKey().KeyChar.ToString();
        }        

        public string ReadResponse()
        {
            Console.WriteLine();
            return Console.ReadLine();
        }

        public string Question(string questionMessage)
        {
            Console.WriteLine(questionMessage);
            Console.WriteLine();

            return ReadResponse();
        }

        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine();
        }

        public void PrintMessage(Message message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("___________");
            Console.WriteLine($"Recived message from {message.Origin}");
            Console.WriteLine($"At {message.Time.ToString("dd\\.hh\\:mm\\:ss")}");
            Console.WriteLine($"Message {message.Id}");
            Console.WriteLine(message.Text);
            Console.WriteLine("___________");
            Console.ResetColor();
        }
    }
}
