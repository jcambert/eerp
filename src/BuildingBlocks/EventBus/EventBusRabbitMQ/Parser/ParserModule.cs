using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ.Parser
{
    public sealed class ParserModule : NinjectModule
    {
        public override void Load()
        {
            //this.Bind(typeof(IFailure<>)).To(typeof(Failure<>));
        }
    }
}
