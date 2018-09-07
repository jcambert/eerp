using EventsBus;
using RabbitMQ.Client;

namespace EventBusRabbitMQ
{
    public interface IRabbitMQPersistentConnection : IPersistentConnection
    {
        IModel CreateModel();
    }
}
