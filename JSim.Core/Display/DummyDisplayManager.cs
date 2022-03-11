using System;

namespace JSim.Core.Display
{
    public class DummyDisplayManager : IDisplayManager
    {
        readonly ILogger logger;

        public DummyDisplayManager(ILogger logger)
        {
            this.logger = logger;
            logger.Log("Dummy display manager created", LogLevel.Debug);
        }

        public void Dispose()
        {
            logger.Log("Dummy display manager disposed", LogLevel.Debug);
        }
    }
}
