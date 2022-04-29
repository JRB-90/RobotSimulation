using JSim.Core.Render;

namespace JSim.Core.Display
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
        ICamera Camera { get; set; }

        /// <summary>
        /// Event fired when the surface is requesting a render.
        /// </summary>
        event RenderRequestedEventHandler? RenderRequested;

        /// <summary>
        /// Forces the surface to render.
        /// </summary>
        void RequestRender();
    }
}
