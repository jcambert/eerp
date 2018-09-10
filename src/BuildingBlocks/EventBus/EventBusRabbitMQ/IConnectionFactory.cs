using Ninject;
using RabbitMQ.Client;
namespace EventBusRabbitMQ
{
    public interface IConnectionFactory
    {
        IConnection CreateConnection();
        ConnectionConfiguration Configuration { get; }
        HostConfiguration Host { get; }

    }
    public class ConnectionFactoryWrapper : IConnectionFactory
    {
        public virtual ConnectionConfiguration Configuration { get; }
        private readonly IKernel _container;
        private readonly RabbitMQ.Client.IConnectionFactory _connectionFactory;

        [Inject]
        public ConnectionFactoryWrapper(IKernel container, ConnectionConfiguration connectionConfiguration)
        {

           
            Configuration = connectionConfiguration;
            _container = container;
           
                var connectionFactory = new ConnectionFactory
                {
                    UseBackgroundThreadsForIO = connectionConfiguration.UseBackgroundThreads,
                    AutomaticRecoveryEnabled = false,
                    TopologyRecoveryEnabled = false
                };

                if (connectionConfiguration.AMQPConnectionString != null)
                {
                    connectionFactory.Uri = connectionConfiguration.AMQPConnectionString;
                }

                connectionFactory.HostName = Configuration.Name;

                if (connectionFactory.VirtualHost == "/")
                    connectionFactory.VirtualHost = Configuration.VirtualHost;

                if (connectionFactory.UserName == "guest")
                    connectionFactory.UserName = Configuration.UserName;

                if (connectionFactory.Password == "guest")
                    connectionFactory.Password = Configuration.Password;

                if (connectionFactory.Port == -1)
                    connectionFactory.Port = Configuration.Port;

                if (Configuration.Ssl.Enabled)
                    connectionFactory.Ssl = Configuration.Ssl;

                //Prefer SSL configurations per each host but fall back to ConnectionConfiguration's SSL configuration for backwards compatibility
                else if (Configuration.Ssl.Enabled)
                    connectionFactory.Ssl = Configuration.Ssl;

                connectionFactory.RequestedHeartbeat = Configuration.RequestedHeartbeat;
                connectionFactory.ClientProperties = Configuration.ClientProperties;
                connectionFactory.AuthMechanisms = Configuration.AuthMechanisms;
            _connectionFactory = connectionFactory;
        }

        public virtual IConnection CreateConnection()
        {
            return _connectionFactory.CreateConnection();
        }



        public HostConfiguration Host => throw new System.NotImplementedException();
    }

    public class ConnectionFactoryInfo
    {
        public ConnectionFactoryInfo(ConnectionFactory connectionFactory, HostConfiguration hostConfiguration)
        {
            ConnectionFactory = connectionFactory;
            HostConfiguration = hostConfiguration;
        }

        public ConnectionFactory ConnectionFactory { get; private set; }
        public HostConfiguration HostConfiguration { get; private set; }
    }
}
