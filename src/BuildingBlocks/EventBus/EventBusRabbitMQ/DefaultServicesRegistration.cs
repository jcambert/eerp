using Ninject;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ
{
    internal static class DefaultServicesRegistration
    {
        internal static void RegisterDefaultServices(this IKernel container)
        {
            container.Bind<IRabbitMQPersistentConnection>().To<DefaultRabbitMQPersistentConnection>();
            container.Bind<IConnectionFactory>().To<ConnectionFactoryWrapper>();
            container.Bind<IPubSub>().To<DefaultPubSub>();
            container.Bind<IRpc>().To<DefaultRpc>();
            container.Bind<ISendReceive>().To<DefaultSendReceive>();
            container.Bind<IScheduler>().To<ExternalScheduler>();
            container.Bind<IAdvancedBus>().To<RabbitAdvancedBus>();
            container.Bind<IBus>().To<RabbitBus>();
        }
    }
}
