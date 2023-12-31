﻿using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasketExample.IntegrationEvents
{
    // Integration Events notes: 
    // An Event is “something that has happened in the past”, therefore its name has to be   
    // An Integration Event is an event that can cause side effects to other microsrvices, Bounded-Contexts or external systems.
    public class ProductPriceChangedIntegrationEvent : IntegrationEvent
    {
        public int ProductId { get; set; }

        public decimal NewPrice { get; set; }

        public decimal OldPrice { get; set; }
    }
}
