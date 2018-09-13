using Polly;
using Polly.Timeout;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventBusRabbitMQ
{
    public class RabbitAdvancedBus : IAdvancedBus
    {
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public async Task PublishAsync(CancellationToken cancellationToken)
        {
            var timeoutPolicy = Policy.TimeoutAsync(TimeSpan.FromSeconds(5), TimeoutStrategy.Optimistic);
            IModel model = null;

            var authPolicy= Policy.Handle<AuthenticationFailureException>().Fallback(() =>
            {

            },
            ex =>
            {
               // Logger.LogError(ex.Message);
            });
            var wrap=Policy.WrapAsync(timeoutPolicy, authPolicy);
            var onAcks = Observable.FromEventPattern<BasicAckEventArgs>(
                e => model.BasicAcks += e,
                e => model.BasicAcks -= e).Select(e => e.EventArgs);
            var onNacks = Observable.FromEventPattern<BasicNackEventArgs>(
               e => model.BasicNacks += e,
               e => model.BasicNacks -= e).Select(e=>e.EventArgs);
            var tokenSource2 = new CancellationTokenSource();
            CancellationToken ct = tokenSource2.Token;

            await wrap.ExecuteAsync(async () =>
            {
                byte[] messageBodyBytes = System.Text.Encoding.UTF8.GetBytes("Bonjour tout le monde!");
                await Task.Factory.StartNew(() =>
                {
                    onAcks.Subscribe(e => tokenSource2.Cancel());
                    model.BasicPublish("", "", null, messageBodyBytes);
                    
                    
                }, ct);


            });


        }
    }
}