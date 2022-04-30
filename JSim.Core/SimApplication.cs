using JSim.Core.Render;
using JSim.Core.SceneGraph;

namespace JSim.Core
{
    /// <summary>
    /// Basic implementation of a simulation application.
    /// </summary>
    public class SimApplication : ISimApplication
    {
        readonly ILogger logger;

        public SimApplication(
            ILogger logger,
            ISceneManager sceneManager,
            ISurfaceManager surfaceManager,
            IRenderingManager renderingManager)
        {
            this.logger = logger;
            SceneManager = sceneManager;
            SurfaceManager = surfaceManager;
            RenderingManager = renderingManager;

            logger.Log("Sim applicaiton created", LogLevel.Debug);
        }

        public ISceneManager SceneManager { get; }

        public ISurfaceManager SurfaceManager { get; }

        public IRenderingManager RenderingManager { get; }

        public void Dispose()
        {
            SceneManager.Dispose();
            RenderingManager.Dispose();
            SurfaceManager.Dispose();
            logger.Log("Sim applicaiton disposed", LogLevel.Debug);
        }
    }
}
