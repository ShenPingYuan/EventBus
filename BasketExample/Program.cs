using BasketExample.IntegrationEvents;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBusRabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BasketExample
{
    class Program
    {
        public static IConfiguration Configuration { get; set; }

        static async Task Main()
        {
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            Configuration = configurationBuilder.Build();

            ServiceCollection serviceCollection = new ServiceCollection();

            ConfigureServices(serviceCollection);

            Console.WriteLine("Hello World!");

            Console.ReadKey();

            await Task.CompletedTask;
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConfiguration(Configuration.GetSection("Logging")).AddConsole());

            services.AddRabbitMQ(Configuration.GetSection("EventBus").Get<RabbitMqOptions>());

            services.AddSingleton<IBasketService, BasketService>();

            services.AddTransient<ProductPriceChangedIntegrationEventHandler>();

            services.AddTransient<ProductPriceChangedDynamicIntegrationEventHandler>();

            var serviceProvider = services.BuildServiceProvider();

            var eventBus = serviceProvider.GetRequiredService<IEventBus>();

            //eventBus.Subscribe<ProductPriceChangedIntegrationEvent, ProductPriceChangedIntegrationEventHandler>();

            eventBus.SubscribeDynamic<ProductPriceChangedDynamicIntegrationEventHandler>("ProductPriceChangedIntegrationEvent");
        }
    }
}
