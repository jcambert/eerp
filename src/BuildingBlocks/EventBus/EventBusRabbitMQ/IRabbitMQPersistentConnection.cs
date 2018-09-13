using EventsBus;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Reactive.Subjects;

namespace EventBusRabbitMQ
{
    public interface IRabbitMQPersistentConnection : IPersistentConnection
    {
        IModel CreateModel();
        void TryClose(bool force=false);
        bool isOpen { get; }
        IObservable<ShutdownEventArgs> WhenConnectionShutdown { get; }
        IObservable<ConnectionBlockedEventArgs> WhenConnectionBlocked { get; }
        IObservable<CallbackExceptionEventArgs> WhenCallbackException { get; }
        IObservable<bool> OnConnexionOpened { get; }
        IObservable<bool> OnReady { get; }
    }
}
