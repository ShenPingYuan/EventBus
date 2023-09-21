using Microsoft.Extensions.Logging;
using ProductExample.IntegrationEvents;
using ProductExample.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductExample
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;

        private readonly ProductContext _dbContext;

        private readonly IProductIntegrationEventService _productIntegrationEventService;

        public ProductService(ILogger<ProductService> logger, ProductContext dbContext, IProductIntegrationEventService productIntegrationEventService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _productIntegrationEventService = productIntegrationEventService;
        }

        public async Task UpdateProductPrice(int id, decimal price)
        {
            var product = _dbContext.Find<Product>(id);

            var oldPrice = product.Price;

            if (oldPrice != price) // Save product's data and publish integration event through the Event Bus if price has changed
            {
                //Create Integration Event to be published through the Event Bus
                var priceChangedEvent = new ProductPriceChangedIntegrationEvent
                {
                    ProductId = id,
                    OldPrice = oldPrice,
                    NewPrice = price
                };

                product.Price = price;

                // Achieving atomicity between original Catalog database operation and the IntegrationEventLog thanks to a local transaction
                await _productIntegrationEventService.SaveProductContextChangesAndEventLogAsync(priceChangedEvent);

                // Publish through the Event Bus and mark the saved event as published
                await _productIntegrationEventService.PublishThroughEventBusAsync(priceChangedEvent);
            }
            else // Just save the updated product because the Product's Price hasn't changed.
            {
                await _dbContext.SaveChangesAsync();
            }

            _logger.LogInformation($"Price from {oldPrice} to {price}.");
        }
    }
}