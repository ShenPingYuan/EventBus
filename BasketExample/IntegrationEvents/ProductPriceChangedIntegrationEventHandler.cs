using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BasketExample.IntegrationEvents
{
    public class ProductPriceChangedIntegrationEventHandler : IIntegrationEventHandler<ProductPriceChangedIntegrationEvent>
    {
        private readonly IBasketService _basketService;

        public ProductPriceChangedIntegrationEventHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task Handle(ProductPriceChangedIntegrationEvent @event)
        {
            await _basketService.UpdateBasketProductPrice(@event.OldPrice, @event.NewPrice);
        }
    }
}
