using Microsoft.Extensions.Logging;

namespace Logger
{
    public class ErrorLogViewModel
    {
        public EventId EventId { get; set; }
        public string Message { get; set; }
        public LogLevel LogLevel { get; set; }
        public object TState { get; set; }
    }
}
