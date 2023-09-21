using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.eShopOnContainers.BuildingBlocks.EventBusRabbitMQ
{
    public class RabbitMqOptions
    {
        public string EventBusHostName { get; set; }

        public string EventBusUserName { get; set; }

        public string EventBusPassword { get; set; }

        public int? EventBusRetryCount { get; set; }

        public string SubscriptionClientName { get; set; }
    }
}
