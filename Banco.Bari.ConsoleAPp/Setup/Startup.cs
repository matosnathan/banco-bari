using Banco.Bari.ConsoleAPp.Commands;
using Banco.Bari.ConsoleAPp.Handlers;
using Banco.Bari.ConsoleAPp.Models;
using Banco.Bari.ConsoleAPp.Models.Settings;
using Banco.Bari.ConsoleAPp.Providers;
using Banco.Bari.ConsoleAPp.Services;
using Bogus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Banco.Bari.ConsoleAPp.Setup
{
    public static class Startup
    {
        public static ServiceProvider InitializeIoC()
        {
            var faker = new Faker();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            var serviceProvider = new ServiceCollection()
            .AddLogging()
            .AddSingleton<IRabbitMQProvider, RabbitMQProvider>()
            .AddSingleton<IUserActionService, UserActionService>()
            .AddSingleton<IConfiguration>(configuration)
            .AddSingleton<App>(new App { Name = faker.Name.JobArea() })
            .AddHandlers();

            return serviceProvider
                .BuildServiceProvider();
        }
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            var consumers = new Consumers();
            consumers.Handlers.Add(new ConsumerModel("IntegrationMessage", typeof(MessageHandler)));
            services.AddSingleton<Consumers>(consumers);
            services.AddSingleton<MessageHandler>();

            return services;
        }
    }
}
