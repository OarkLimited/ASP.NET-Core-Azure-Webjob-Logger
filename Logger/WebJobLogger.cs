using Logger;
using Microsoft.Extensions.Logging;
using System;
//using FrameworkLogger = Microsoft.Extensions.Logging.ILogger;

namespace Logger
{
    public class WebJobLogger : ILogger
    {
        private string _categoryName;
        private Func<string, LogLevel, bool> _filter;
        private readonly ICustomErrorLogger _customErrorLogger;

        public WebJobLogger(string categoryName, Func<string, LogLevel, bool> filter, ICustomErrorLogger customErrorLogger)
        {
            _categoryName = categoryName;
            _filter = filter;
            _customErrorLogger = customErrorLogger;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            // Not necessary
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return (_filter == null || _filter(_categoryName, logLevel));
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            var message = formatter(state, exception);

            if (string.IsNullOrEmpty(message))
            {
                return;
            }



            //message = $@"Level: {logLevel} {message}";

            if (exception != null)
            {
                message += Environment.NewLine + Environment.NewLine + exception.ToString();
            }






            _customErrorLogger.ErrorLogger(new ErrorLogViewModel
            {
                LogLevel = logLevel,
                EventId = eventId,
                TState = state,
                Message = message
            });

        }
    }

    
}
