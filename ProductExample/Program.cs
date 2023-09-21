using ProductExample.IntegrationEvents;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBusRabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductExample.Models;
using Microsoft.eShopOnContainers.BuildingBlocks.IntegrationEventLogEF;
using System.Reflection;
using System.Data.Common;
using Microsoft.eShopOnContainers.BuildingBlocks.IntegrationEventLogEF.Services;

namespace ProductExample
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

            var serviceProvider = serviceCollection.BuildServiceProvider();

            //serviceProvider.GetService<IntegrationEventLogContext>().Database.Migrate();
            //serviceProvider.GetService<ProductContext>().Database.Migrate();

            //var productService = serviceProvider.GetRequiredService<IProductService>();

            //Random random = new Random();

            //while (true)
            //{
            //    await productService.UpdateProductPrice(123, random.Next(10, 99));
            //    await Task.Delay(TimeSpan.FromSeconds(3));
            //}

            var productService = serviceProvider.GetRequiredService<IProductService>();

            Random random = new Random();

            while (true)
            {
                await productService.UpdateProductPrice(123, random.Next(10, 99));
                await Task.Delay(TimeSpan.FromSeconds(3));
            }
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConfiguration(Configuration.GetSection("Logging")).AddConsole());

            //services.AddDbContext<ProductContext>(options =>
            //{
            //    options.UseSqlServer(Configuration.GetConnectionString("product"), sqlOptions =>
            //    {
            //        sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
            //        sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            //    });
            //});

            //services.AddDbContext<IntegrationEventLogContext>(options =>
            //{
            //    options.UseSqlServer(Configuration.GetConnectionString("product"), sqlOptions =>
            //    {
            //        sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
            //        sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            //    });
            //});

            services.AddSingleton<IProductService, ProductController>();

            services.AddRabbitMQ(Configuration.GetSection("EventBus").Get<RabbitMqOptions>());

            //services.AddSingleton<IProductService, ProductService>();

            //services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(sp => (DbConnection c) => new IntegrationEventLogService(c));

            //services.AddTransient<IProductIntegrationEventService, ProductIntegrationEventService>();
        }
    }
}
