using JSim.Core.Display;
using JSim.Core.SceneGraph;

namespace JSim.Core.Render
{
    public class DummyRenderingManager : IRenderingManager
    {
        readonly ILogger logger;

        public DummyRenderingManager(ILogger logger)
        {
            this.logger = logger;
            logger.Log("Dummy rendering manager created", LogLevel.Debug);
        }

        public void Dispose()
        {
            logger.Log("Dummy rendering manager disposed", LogLevel.Debug);
        }

        public void Render(
            IRenderingSurface surface,
            IScene scene)
        {
            logger.Log("Render called", LogLevel.Debug);
        }
    }
}
