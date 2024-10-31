using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kgm.ExampleNetEight.FunctionApp.Tests.Fakes.Logger
{
    public abstract class FakeLogger<T> : ILogger<T>
    {
        public abstract void Log(LogLevel logLevel, string message, Exception? exception = null);

        public void Log<TState>(LogLevel logLevel,EventId eventId,TState state,Exception? exception, Func<TState, Exception?, string> formatter)
        {
            var message = formatter(state, exception);
            this.Log(logLevel, message, exception);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        IDisposable ILogger.BeginScope<TState>(TState state)
        {
            return this.BeginScope(state);
        }

        public abstract IDisposable BeginScope<TState>(TState state);
    }
}
