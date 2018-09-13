using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ
{
    public static class RabbitBusExtensions
    {
        public static void publish(this IBus bus,string message)
        {
            bus.PubSub.publish(message);
        }

        
    }
}
