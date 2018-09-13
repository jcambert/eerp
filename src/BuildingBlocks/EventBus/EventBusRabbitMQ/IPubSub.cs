namespace EventBusRabbitMQ
{
    public interface IPubSub
    {
        void publish(string message);

        IRabbitMQPersistentConnection Connection { get;  }
    }
}