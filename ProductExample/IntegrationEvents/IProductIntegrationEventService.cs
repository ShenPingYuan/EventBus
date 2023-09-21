using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductExample.IntegrationEvents
{
    public interface IProductIntegrationEventService
    {
        Task SaveProductContextChangesAndEventLogAsync(IntegrationEvent evt);

        Task PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}
