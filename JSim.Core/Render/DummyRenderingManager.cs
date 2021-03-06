using JSim.Core.SceneGraph;

namespace JSim.Core.Render
{
    /// <summary>
    /// Provides a dummy rendering engine implementation.
    /// </summary>
    public class DummyRenderingEngine : IRenderingEngine
    {
        readonly ILogger logger;

        public DummyRenderingEngine(ILogger logger)
        {
            this.logger = logger;
            logger.Log("Dummy rendering engine created", LogLevel.Debug);
        }

        public void Dispose()
        {
            logger.Log("Dummy rendering engine disposed", LogLevel.Debug);
        }

        public void Render(
            IRenderingSurface surface,
            IScene? scene)
        {
            logger.Log("Render called", LogLevel.Debug);
        }
    }
}
