using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ.Parser
{
    public interface IFailure<out T> : IResult<T>
    {
        string Message { get; }
        IEnumerable<string> Expectations { get; }
        Input FailedInput { get; }
    }
}
