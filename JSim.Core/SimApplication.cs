using JSim.Core.Display;
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
            IDisplayManager displayManager,
            IRenderingManager renderingManager)
        {
            this.logger = logger;
            SceneManager = sceneManager;
            DisplayManager = displayManager;
            RenderingManager = renderingManager;

            DisplayManager.SurfaceRequiresRender += OnRenderRequested;
            logger.Log("Sim applicaiton created", LogLevel.Debug);
        }

        public ISceneManager SceneManager { get; }

        public IDisplayManager DisplayManager { get; }

        public IRenderingManager RenderingManager { get; }

        public void Dispose()
        {
            SceneManager.Dispose();
            RenderingManager.Dispose();
            DisplayManager.Dispose();
            logger.Log("Sim applicaiton disposed", LogLevel.Debug);
        }

        private void OnRenderRequested(object sender, SurfaceRequiresRenderEventArgs e)
        {
            RenderingManager.Render(
                e.Surface,
                SceneManager.CurrentScene
            );
        }
    }
}
