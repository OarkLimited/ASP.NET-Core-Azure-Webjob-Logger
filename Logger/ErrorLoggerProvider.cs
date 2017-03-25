using Microsoft.Extensions.Logging;
using System;


namespace Logger
{

    public class ErrorLoggerProvider : ILoggerProvider
    {
        private readonly Func<string, LogLevel, bool> _filter;
        private readonly ICustomErrorLogger _customErrorLogger;



        public ErrorLoggerProvider(Func<string, LogLevel, bool> filter, ICustomErrorLogger customErrorLogger)
        {
            _customErrorLogger = customErrorLogger;
            _filter = filter;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new WebJobLogger(categoryName, _filter, _customErrorLogger);
        }

        public void Dispose()
        {
        }
    }
}
