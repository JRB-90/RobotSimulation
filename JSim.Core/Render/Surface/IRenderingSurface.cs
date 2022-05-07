using JSim.Core.SceneGraph;

namespace JSim.Core.Render
{
    /// <summary>
    /// Defines a onscreen surface that can be renderered to.
    /// </summary>
    public interface IRenderingSurface : IDisposable
    {
        /// <summary>
        /// Gets the surfaces renderable width.
        /// </summary>
        int SurfaceWidth { get; }

        /// <summary>
        /// Gets the surfaces renderable height.
        /// </summary>
        int SurfaceHeight { get; }

        /// <summary>
        /// The camera for the viewport.
        /// </summary>
        ICamera? Camera { get; set; }

        /// <summary>
        /// Scene associated with this surface.
        /// Will be the scene render when the surface is render is requested.
        /// </summary>
        IScene? Scene { get; set; }

        /// <summary>
        /// Forces the surface to render.
        /// </summary>
        void RequestRender();
    }
}
