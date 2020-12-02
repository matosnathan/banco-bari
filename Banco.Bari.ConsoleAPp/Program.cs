using Banco.Bari.ConsoleAPp.Models.Settings;
using Banco.Bari.ConsoleAPp.Providers;
using Banco.Bari.ConsoleAPp.Services;
using Banco.Bari.ConsoleAPp.Setup;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Banco.Bari.ConsoleAPp
{
    class Program
    {
        /*
         * PUBLISH POR TOPIC
         * CONFIGURAR DICTIONARY DE HANDLERS COM KEY TOPIC
         * EX: ("SYTEM_TOPIC", typeof(MessageHandler))
         */
        private static IUserActionService _actionService;
        private static IRabbitMQProvider _mqProvider;
        private static App _app;
        private const string SYSTEM_TOPIC = "IntegrationMessage";
        static void Main(string[] args)
        {
            Initialize();

            Console.WriteLine($"Bem vindo ao sistema {_app.Name}");

            string option = "";
            while (option != "0")
            {
                switch (option)
                {
                    case "1":
                        _mqProvider.Publish(_actionService.SendMessage(), SYSTEM_TOPIC);
                        break;
                    case "2":
                        _mqProvider.Subscribe(SYSTEM_TOPIC);
                        break;
                }
                    
                option = _actionService.Menu().KeyChar.ToString();                
            }
        }


        private static void Initialize()
        {
            var services = Startup.InitializeIoC();
            
            _actionService = services.GetService<IUserActionService>();
            _mqProvider = services.GetService<IRabbitMQProvider>();
            _app = services.GetService<App>();

            
        }
    }
}
