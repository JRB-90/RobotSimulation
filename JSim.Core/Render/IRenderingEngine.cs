using JSim.Core.Display;
using JSim.Core.SceneGraph;

namespace JSim.Core.Render
{
    /// <summary>
    /// Interface to define the behaviour of all rendering engines.
    /// Rendering managers are responsible for rendering scene graphs using
    /// their specific rendering implementation. This may involve creating and
    /// maintaining GPU contexts/GPU memory management.
    /// </summary>
    public interface IRenderingEngine : IDisposable
    {
        /// <summary>
        /// Renders a scene to the given surface.
        /// </summary>
        /// <param name="surface">Renderable surface to draw onto.</param>
        /// <param name="scene">Scene graph to render.</param>
        void Render(
            IRenderingSurface surface,
            IScene scene
        );
    }
}
