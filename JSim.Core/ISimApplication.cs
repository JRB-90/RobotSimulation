using JSim.Core.SceneGraph;
using JSim.Core.Render;

namespace JSim.Core
{
    /// <summary>
    /// Interface defining the behaviour of all JSim applications.
    /// An application is responsible for initialising all the components needed
    /// to run a robotic simulation and providing a single point of contact for
    /// external interaction with the application.
    /// </summary>
    public interface ISimApplication : IDisposable
    {
        /// <summary>
        /// Gets the ISceneManager implementation used in the JSim application,
        /// which is used for interacting with the scene graph.
        /// </summary>
        ISceneManager SceneManager { get; }

        /// <summary>
        /// Gets the IDisplayManager implementation used in the JSim application,
        /// which is used to interact with display surfaces.
        /// </summary>
        ISurfaceManager SurfaceManager { get; }

        /// <summary>
        /// Gets the IRenderingManager implementation used in the JSim application,
        /// which is used to render scenes to a display surface.
        /// </summary>
        IRenderingManager RenderingManager { get; }
    }
}
