using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTest
{
    public class UnitTestLogger<T> : ILogger<T>, IDisposable
    {
        Action<string> output;
        public UnitTestLogger(Action<string> output)
        {
            this.output = output;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }

        public void Dispose()
        {
            
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            this.output(state.ToString());
        }
    }
}
