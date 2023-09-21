using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BasketExample
{
    public class BasketService : IBasketService
    {
        private readonly ILogger<BasketService> _logger;

        public BasketService(ILogger<BasketService> logger)
        {
            _logger = logger;
        }

        public async Task UpdateBasketProductPrice(decimal oldPrice, decimal newPrice)
        {
            _logger.LogInformation($"Price from {oldPrice} to {newPrice}.");

            await Task.CompletedTask;
        }
    }
}
