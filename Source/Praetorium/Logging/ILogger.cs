using System;
namespace Praetorium.Logging
{
    public interface ILogger
    {

        /// <summary>
        /// Determines if the <paramref name="logLevel"/> is enabled for logging.
        /// </summary>
        /// <param name="logLevel">The level (priority) of the log message.</param>
        /// <returns>
        /// True if the <paramref name="logLevel"/> is enabled; otherwise, false.
        /// </returns>
        bool IsLoggable(LogLevel logLevel);

        /// <summary>
        /// Logs the <paramref name="message"/> using the specified <paramref name="logLevel"/>.
        /// </summary>
        /// <param name="logLevel">The level (priority) of the log message.</param>
        /// <param name="message">The message format.</param>
        /// <param name="args">An array of zero or more objects to format with the <paramref name="message"/>.</param>
        void Log(LogLevel logLevel, string message, params object[] args);

        void Log(Exception exception);

        void Log(LogLevel logLevel, Exception exception);

        void Log(LogLevel logLevel, Exception exception, string message, params object[] args);

        /// <summary>
        /// Logs a message using the specified <paramref name="logEntry"/>.
        /// </summary>
        /// <param name="logEntry">The log entry.</param>
        void Log(ILogEntry logEntry);

    }
}
