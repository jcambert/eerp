using Ninject;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ
{
    public static class Rabbit
    {
        

        static Rabbit()
        {
            Kernel = new StandardKernel();
            Kernel.RegisterDefaultServices();
        }

        public static IKernel Kernel { get; }

        public static IBus CreateBus(string connectionString)
        {
            Kernel.Bind<IConnectionFactory>().To<ConnectionFactoryWrapper>().WithConstructorArgument("connectionString",connectionString);
            var bus = Kernel.Get<IBus>();
            return bus;
        }

        
    }
}
