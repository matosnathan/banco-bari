using Banco.Bari.ConsoleApp.Domain.Models;
using Banco.Bari.ConsoleApp.Handlers;
using Banco.Bari.ConsoleApp.Models;
using Banco.Bari.ConsoleApp.Providers;
using Banco.Bari.ConsoleApp.Services;
using Bogus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Banco.Bari.ConsoleApp.Setup
{
    public static class Startup
    {
        public static ServiceProvider InitializeIoC()
        {
            var serviceProvider = new ServiceCollection()
                .AddAppConfiguration()
                .AddLogging()
                .AddProviders()
                .AddServices()
                .AddHandlers();

            return serviceProvider
                .BuildServiceProvider();
        }
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddSingleton<MessageHandler>();
            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IWritterService, WritterService>();
            services.AddSingleton<IMessagingFacade, MessagingFacade>();
            return services;
        }

        public static IServiceCollection AddProviders(this IServiceCollection services)
        {
            services.AddSingleton<IRabbitMQProvider, RabbitMQProvider>();
            return services;
        }

        public static IServiceCollection AddAppConfiguration(this IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json");

            var faker = new Faker();

            var configuration = builder.Build();

            var mqConnection = new MQConnection(configuration.GetSection("RabbitMQ:Host").Value);

            services
            .AddSingleton<IConfiguration>(configuration)
            .AddSingleton<App>(new App(faker.Name.JobArea()))
            .AddSingleton<MQConnection>(mqConnection);
            

            return services;
        }

    }
}
