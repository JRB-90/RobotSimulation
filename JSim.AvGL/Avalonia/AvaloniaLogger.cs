using JSim.Core;
using System.Diagnostics;

namespace JSim.AvGL
{
    internal class AvaloniaLogger : ILogger
    {
        public void Dispose()
        {
        }

        public void Log(string logMessage, LogLevel logLevel)
        {
            Trace.WriteLine(logMessage, logLevel.ToString());
        }
    }
}
