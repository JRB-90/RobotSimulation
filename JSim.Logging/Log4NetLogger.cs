using JSim.Core;
using log4net;
using log4net.Config;
using System.Reflection;
using System.Xml;

namespace JSim.Logging
{
    /// <summary>
    /// Logger implementation class for using a log4net logger.
    /// </summary>
    public class Log4NetLogger : ILogger
    {
        /// <summary>
        /// Constructs a new log4net logging implementation.
        /// </summary>
        /// <param name="xmlDocument">Xml document containing the log4net configuration.</param>
        public Log4NetLogger(XmlDocument xmlDocument)
        {
            if (xmlDocument.DocumentElement == null)
            {
                throw new ArgumentException("Cannot find a valid root node in XML config");
            }

            var log4netNode = (XmlElement?)xmlDocument.DocumentElement.SelectSingleNode("log4net");

            if (log4netNode == null)
            {
                throw new ArgumentException("Cannot find log4net node in XML config");
            }

            log4net.Repository.ILoggerRepository logRepository =
                LogManager.GetRepository(Assembly.GetEntryAssembly());

            XmlConfigurator.Configure(
                logRepository,
                log4netNode
            );

            GlobalContext.Properties["host"] = Environment.MachineName;
        }

        /// <summary>
        /// Runs on disposing of object.
        /// </summary>
        public void Dispose()
        {
            log.Info("Logger shutting down...");
            log.Info("\n\n");
        }

        /// <summary>
        /// Creates a new log message with the given severity.
        /// </summary>
        /// <param name="logMessage">Message string to log.</param>
        /// <param name="logLevel">Severity level of the log message.</param>
        public void Log(string logMessage, LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    log.Debug(logMessage);
                    break;
                case LogLevel.Info:
                    log.Info(logMessage);
                    break;
                case LogLevel.Warning:
                    log.Warn(logMessage);
                    break;
                case LogLevel.Error:
                    log.Error(logMessage);
                    break;
                case LogLevel.Fatal:
                    log.Fatal(logMessage);
                    break;
                default:
                    log.Error("Failed to log message, log level not supported in Log4NetFileLogger");
                    break;
            }
        }

        static Log4NetLogger()
        {
            var methodBase = MethodBase.GetCurrentMethod();
            if (methodBase == null)
            {
                throw new InvalidOperationException("Could not get the current method base info");
            }

            var declType = methodBase.DeclaringType;
            if (declType == null)
            {
                throw new InvalidOperationException("Could not get the declaring type");
            }

            log = LogManager.GetLogger(declType);
        }

        private static readonly ILog log;
    }
}
