using Ninject;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ
{
    public class RabbitBus : IBus
    {
        private readonly IPubSub _pubsub;
        private readonly IRpc _rpc;
        private readonly ISendReceive _sendreceive;
        private readonly IScheduler _scheduler;
        private readonly IAdvancedBus _advanced;
        private readonly IRabbitMQPersistentConnection _connection;

        [Inject]
        public RabbitBus(
            IAdvancedBus advanced,
            IPubSub pubSub,
            IRpc rpc,
            ISendReceive sendReceive,
            IScheduler scheduler,
            IRabbitMQPersistentConnection connection)
        {

        }
        public IPubSub PubSub => _pubsub;

        public IRpc Rpc => _rpc;

        public ISendReceive SendReceive => _sendreceive;

        public IScheduler Scheduler => _scheduler;

        public IAdvancedBus Advanced => _advanced;

        public IRabbitMQPersistentConnection Connection => _connection;

        public void Dispose()
        {
            Advanced.Dispose();
        }
    }
}
