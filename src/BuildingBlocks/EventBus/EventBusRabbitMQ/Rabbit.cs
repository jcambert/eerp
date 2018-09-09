using Ninject;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ
{
    public static class Rabbit
    {
        static IKernel Kernel => new StandardKernel();

        public static IBus CreateBus(string connectionString)
        {
            Kernel.RegisterDefaultServices();
            return Kernel.Get<IBus>();
        }

        
    }
}
