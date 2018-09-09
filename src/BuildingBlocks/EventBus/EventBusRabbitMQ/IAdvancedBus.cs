using System;
using System.Threading;
using System.Threading.Tasks;

namespace EventBusRabbitMQ
{
    public interface IAdvancedBus : IDisposable
    {
        Task PublishAsync(CancellationToken cancellationToken );
    }
}