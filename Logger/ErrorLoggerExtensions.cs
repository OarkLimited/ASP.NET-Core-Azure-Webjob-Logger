using Microsoft.Extensions.Logging;
using System;

namespace Logger
{
    public static class ErrorLoggerExtensions
    {
        public static ILoggerFactory AddWebJob(this ILoggerFactory factory,
                                              ICustomErrorLogger customErrorLogger,
                                              Func<string, LogLevel, bool> filter = null)
        {
            factory.AddProvider(new ErrorLoggerProvider(filter, customErrorLogger));
            return factory;
        }

        public static ILoggerFactory AddWebJob(this ILoggerFactory factory, ICustomErrorLogger customErrorLogger, LogLevel minLevel)
        {
            return AddWebJob(
                factory,
                customErrorLogger,
                (_, logLevel) => logLevel >= minLevel);
        }
    }
}
