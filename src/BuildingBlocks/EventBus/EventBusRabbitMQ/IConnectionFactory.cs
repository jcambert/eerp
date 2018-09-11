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
        public ConnectionFactoryWrapper(string connectionString)
        {


            Configuration = new ConnectionConfiguration();


            var connectionFactory = new ConnectionFactory
            {
                UseBackgroundThreadsForIO = Configuration.UseBackgroundThreads,
                AutomaticRecoveryEnabled = false,
                TopologyRecoveryEnabled = false
            };

            if (Configuration.AMQPConnectionString != null)
            {
                connectionFactory.Uri = Configuration.AMQPConnectionString;
            }

            connectionFactory.HostName = Configuration.Name;

            if (Configuration.VirtualHost == "/")
                connectionFactory.VirtualHost = Configuration.VirtualHost;

            if (Configuration.UserName == "guest")
                connectionFactory.UserName = Configuration.UserName;

            if (Configuration.Password == "guest")
                connectionFactory.Password = Configuration.Password;

            if (Configuration.Port == -1)
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

        [Inject]
        public IKernel Container { get; set; }
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
