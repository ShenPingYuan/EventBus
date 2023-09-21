using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.eShopOnContainers.BuildingBlocks.IntegrationEventLogEF;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProductExample.Models
{
    public class ProductDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ProductContext>
    {
        public ProductContext CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            var configuration = configurationBuilder.Build();

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ProductContext>();

            dbContextOptionsBuilder.UseSqlServer(configuration.GetConnectionString("product"));

            return new ProductContext(dbContextOptionsBuilder.Options);
        }

    }

    public class EventLogDesignTimeDbContextFactory : IDesignTimeDbContextFactory<IntegrationEventLogContext>
    {
        public IntegrationEventLogContext CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            var configuration = configurationBuilder.Build();

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<IntegrationEventLogContext>();

            dbContextOptionsBuilder.UseSqlServer(configuration.GetConnectionString("product"), options => options.MigrationsAssembly(GetType().Assembly.GetName().Name));

            return new IntegrationEventLogContext(dbContextOptionsBuilder.Options);
        }
    }
}