using System;

namespace JSim.Core.SceneGraph
{
    public class DummySceneManager : ISceneManager
    {
        readonly ILogger logger;

        public DummySceneManager(ILogger logger)
        {
            this.logger = logger;
            logger.Log("Dummy scene manager created", LogLevel.Debug);
        }

        public void Dispose()
        {
            logger.Log("Dummy scene manager disposed", LogLevel.Debug);
        }
    }
}
