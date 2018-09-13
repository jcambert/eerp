using EventBusRabbitMQ.ConnectionString;
using Microsoft.Extensions.Logging;
using Ninject;

namespace EventBusRabbitMQ
{
    internal static class DefaultServicesRegistration
    {
        internal static void RegisterDefaultServices(this IKernel container)
        {
            container.Bind<IRabbitMQPersistentConnection>().To<DefaultRabbitMQPersistentConnection>();
            container.Bind<IConnectionStringParser>().To<ConnectionStringParser>();
            container.Bind<IPubSub>().To<DefaultPubSub>();
            container.Bind<IRpc>().To<DefaultRpc>();
            container.Bind<ISendReceive>().To<DefaultSendReceive>();
            container.Bind<IScheduler>().To<ExternalScheduler>();
            container.Bind<IAdvancedBus>().To<RabbitAdvancedBus>();
            container.Bind<IBus>().To<RabbitBus>();
            container.Bind<ILogger>().ToMethod((kernel) =>
            {
                ILoggerFactory loggerFactory = new LoggerFactory().AddConsole().AddDebug();
                ILogger logger = loggerFactory.CreateLogger("EventBusRabbitMQ");
                return logger;

            });
        }
    }
}
