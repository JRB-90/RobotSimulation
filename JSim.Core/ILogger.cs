namespace JSim.Core
{
    /// <summary>
    /// Interface defining the behaviour of a JSim logger.
    /// </summary>
    public interface ILogger : IDisposable
    {
        /// <summary>
        /// Creates a new log message with the given severity.
        /// </summary>
        /// <param name="logMessage">Message string to log.</param>
        /// <param name="logLevel">Severity level of the log message.</param>
        void Log(string logMessage, LogLevel logLevel);
    }
}
