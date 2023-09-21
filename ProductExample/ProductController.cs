using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using ProductExample.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductExample
{
    public class ProductController : IProductService
    {
        private readonly IEventBus _eventBus;

        private readonly ILogger<ProductController> _logger;

        public ProductController(IEventBus eventBus, ILogger<ProductController> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task UpdateProductPrice(int id, decimal price)
        {
            var @event = new ProductPriceChangedIntegrationEvent
            {
                ProductId = 123,
                OldPrice = 22.22M,
                NewPrice = price
            };

            _eventBus.Publish(@event);

            _logger.LogInformation($"Price from {@event.OldPrice} to {price}.");

            await Task.CompletedTask;
        }
    }
}
