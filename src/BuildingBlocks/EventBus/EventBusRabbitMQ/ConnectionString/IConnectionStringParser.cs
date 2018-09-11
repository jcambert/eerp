using EventBusRabbitMQ.Parser;
using System;
using System.Linq;
namespace EventBusRabbitMQ.ConnectionString
{
    public interface IConnectionStringParser
    {
        ConnectionConfiguration Parse(string connectionString);
    }

    public class ConnectionStringParser : IConnectionStringParser
    {
        public ConnectionConfiguration Parse(string connectionString)
        {
            try
            {
                var updater = ConnectionStringGrammar.ConnectionStringBuilder.Parse(connectionString);
                var connectionConfiguration = updater.Aggregate(new ConnectionConfiguration(), (current, updateFunction) => updateFunction(current));
                connectionConfiguration.Validate();
                return connectionConfiguration;
            }
            catch (Exception parseException)
            {
                throw new Exception($"Connection String { parseException.Message}");
            }
        }
    }
}
