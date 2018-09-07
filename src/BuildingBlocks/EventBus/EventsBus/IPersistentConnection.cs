using System;

namespace EventsBus
{
    public interface IPersistentConnection:IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

    }
}
