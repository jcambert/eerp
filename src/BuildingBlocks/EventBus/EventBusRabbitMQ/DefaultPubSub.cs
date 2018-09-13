using Microsoft.Extensions.Logging;
using Ninject;
using Polly;
using RabbitMQ.Client.Exceptions;
using System;
using System.Text;

namespace EventBusRabbitMQ
{
    public class DefaultPubSub : IPubSub
    {
        [Inject]
        public DefaultPubSub(IRabbitMQPersistentConnection connection,IConnectionFactory factory, ILogger logger)
        {
            Connection = connection;
            Logger = logger;
            ConnectionConfiguration = factory.Configuration;
        }
        public IRabbitMQPersistentConnection Connection { get; }

        public ILogger Logger { get; }

        private readonly ConnectionConfiguration ConnectionConfiguration;

        public void publish(string message)
        {
            var s = Connection.OnReady.Subscribe(m =>
              {
                  var policy = Policy.Handle<AuthenticationFailureException>().Fallback(() =>
                   {

                   },
                      ex =>
                      {
                          Logger.LogError(ex.Message);
                      });
                  policy.Execute(() =>
                  {
                      var channel = Connection.CreateModel();
                      channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
                      var properties = channel.CreateBasicProperties();
                      channel.BasicAcks += Channel_BasicAcks;
                      channel.ConfirmSelect();
                      channel.BasicPublish("", "hello", true, null, Encoding.UTF8.GetBytes(message));
                      channel.WaitForConfirms();
                      Logger.LogInformation($"Publish {message} to channel 'Hello'");
                  });

              });

            Connection.TryConnect();
            
        }

        private void Channel_BasicAcks(object sender, RabbitMQ.Client.Events.BasicAckEventArgs e)
        {
            Logger.LogInformation($"The broker confirm received message");
        }

        private void internalPublish(string msg)
        {

        }
    }
}