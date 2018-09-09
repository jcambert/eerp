using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.IO;
using System.Net.Sockets;
using System.Reactive.Linq;

namespace EventBusRabbitMQ
{
    public class DefaultRabbitMQPersistentConnection : IRabbitMQPersistentConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger<DefaultRabbitMQPersistentConnection> _logger;
        private readonly int _retryCount;
        IConnection _connection;
        bool _disposed;

        object sync_root = new object();

        private IDisposable whenConnectionShutdown;
        private IDisposable whenConnectionBlocked;
        private IDisposable whenCallbackException;

        public DefaultRabbitMQPersistentConnection(IConnectionFactory connectionFactory, ILogger<DefaultRabbitMQPersistentConnection> logger, int retryCount = 5)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _retryCount = retryCount;


        }

        public bool IsConnected
        {
            get
            {
                return _connection != null && _connection.IsOpen && !_disposed;
            }
        }

        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
            }

            return _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;

            try
            {
                whenCallbackException.Dispose();
                whenConnectionBlocked.Dispose();
                whenConnectionShutdown.Dispose();
                _connection.Dispose();
            }
            catch (IOException ex)
            {
                _logger.LogCritical(ex.ToString());
            }
        }

        public bool TryConnect()
        {
            _logger.LogInformation("RabbitMQ Client is trying to connect");

            lock (sync_root)
            {
                var policy = RetryPolicy.Handle<SocketException>()
                    .Or<BrokerUnreachableException>()
                    .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                    {
                        _logger.LogWarning(ex.ToString());
                    }
                );

                policy.Execute(() =>
                {
                    _connection = _connectionFactory
                          .CreateConnection();
                });

                if (IsConnected)
                {
                    whenConnectionShutdown = this.WhenConnectionShutdown.Subscribe(e =>
                    {
                        if (_disposed) return;

                        _logger.LogWarning("A RabbitMQ connection is on shutdown. Trying to re-connect...");

                        TryConnect();
                    });
                    whenCallbackException = this.WhenCallbackException.Subscribe(e =>
                    {
                        if (_disposed) return;

                        _logger.LogWarning("A RabbitMQ connection throw exception. Trying to re-connect...");

                        TryConnect();
                    });
                    whenConnectionBlocked = this.WhenConnectionBlocked.Subscribe(e =>
                    {
                        if (_disposed) return;

                        _logger.LogWarning("A RabbitMQ connection is blocked. Trying to re-connect...");

                        TryConnect();
                    });

                    //_connection.ConnectionShutdown += OnConnectionShutdown;
                    // _connection.CallbackException += OnCallbackException;
                    // _connection.ConnectionBlocked += OnConnectionBlocked;

                    _logger.LogInformation($"RabbitMQ persistent connection acquired a connection {_connection.Endpoint.HostName} and is subscribed to failure events");

                    return true;
                }
                else
                {
                    _logger.LogCritical("FATAL ERROR: RabbitMQ connections could not be created and opened");

                    return false;
                }
            }
        }

        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            if (_disposed) return;

            _logger.LogWarning("A RabbitMQ connection is shutdown. Trying to re-connect...");

            TryConnect();
        }

        void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            if (_disposed) return;

            _logger.LogWarning("A RabbitMQ connection throw exception. Trying to re-connect...");

            TryConnect();
        }

        void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
        {
            if (_disposed) return;

            _logger.LogWarning("A RabbitMQ connection is on shutdown. Trying to re-connect...");

            TryConnect();
        }

        public IObservable<ShutdownEventArgs> WhenConnectionShutdown
        {
            get
            {
                return Observable
                    .FromEventPattern<ShutdownEventArgs>(
                        h => _connection.ConnectionShutdown += h,
                        h => _connection.ConnectionShutdown -= h)
                        .Select(e => e.EventArgs);
            }
        }

        public IObservable<ConnectionBlockedEventArgs> WhenConnectionBlocked
        {
            get
            {
                return Observable
                    .FromEventPattern<ConnectionBlockedEventArgs>(
                        h => _connection.ConnectionBlocked += h,
                        h => _connection.ConnectionBlocked -= h)
                        .Select(e => e.EventArgs);
            }
        }

        public IObservable<CallbackExceptionEventArgs> WhenCallbackException
        {
            get
            {
                return Observable
                    .FromEventPattern<CallbackExceptionEventArgs>(
                        h => _connection.CallbackException += h,
                        h => _connection.CallbackException -= h)
                        .Select(e => e.EventArgs);
            }
        }
    }
}
