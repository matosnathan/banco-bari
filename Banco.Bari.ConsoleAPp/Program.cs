using Banco.Bari.ConsoleApp.Handlers;
using Banco.Bari.ConsoleApp.Models;
using Banco.Bari.ConsoleApp.Providers;
using Banco.Bari.ConsoleApp.Services;
using Banco.Bari.ConsoleApp.Setup;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reactive.Linq;
using System.Threading;

namespace Banco.Bari.ConsoleApp
{
    class Program
    {
        private static IWritterService _writterService;
        private static IRabbitMQProvider _mqProvider;
        private static App _app;
        private const string SYSTEM_TOPIC = "IntegrationMessage";
        static void Main(string[] args)
        {
            Initialize();

            _writterService.PrintMessage($"Bem vindo ao sistema {_app.Name}");

            var option = _writterService.Menu();

            SwitchMenu(option);
        }

        private static void SwitchMenu(string option)
        {
            switch (option)
            {
                case "1":
                    SendMessages();
                    break;
                case "2":
                    AutomaticMessages();
                    break;
                case "3":
                    Listen();
                    break;
            }
        }

        private static void SendMessages()
        {
            var message = "";
            _writterService.PrintMessage("A seguir digite a mensagem que deseja enviar ou 0 para sair");

            while (!message.Equals("0"))
            {
                if (!string.IsNullOrEmpty(message))
                {
                    _mqProvider.Publish(message, SYSTEM_TOPIC);
                }

                message = _writterService.ReadResponse();                    
            }
        }

        private static void AutomaticMessages()
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));
            Console.Write(".");
            _mqProvider.Publish("Hello World", SYSTEM_TOPIC);
            AutomaticMessages();
        }

        private static void Listen()
        {
            _mqProvider.Subscribe<MessageHandler>(SYSTEM_TOPIC);
            _writterService.ReadResponse();
        }

        private static void Initialize()
        {
            var services = Startup.InitializeIoC();

            _writterService = services.GetService<IWritterService>();
            _mqProvider = services.GetService<IRabbitMQProvider>();
            _app = services.GetService<App>();
        }
    }
}
