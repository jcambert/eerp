using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ.Parser
{
    public interface ISuccess<out T> : IResult<T>
    {
        T Result { get; }
        Input Remainder { get; }
    }
}
