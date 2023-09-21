using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BasketExample.IntegrationEvents
{
    public class ProductPriceChangedDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler
    {
        private readonly IBasketService _basketService;

        public ProductPriceChangedDynamicIntegrationEventHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task Handle(dynamic eventData)
        {
            decimal oldPrice = Convert.ToDecimal(eventData.OldPrice.ToString());
            decimal newPrice = Convert.ToDecimal(eventData.NewPrice.ToString());

            await _basketService.UpdateBasketProductPrice(oldPrice, newPrice);
        }
    }
}
