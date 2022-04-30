namespace JSim.Core.Render
{
    /// <summary>
    /// Standard implementation of a rendering manager.
    /// </summary>
    public class RenderingManager : IRenderingManager
    {
        public RenderingManager(IRenderingEngine renderingEngine)
        {
            RenderingEngine = renderingEngine;
        }

        /// <summary>
        /// Rendering engine configured for this application.
        /// </summary>
        public IRenderingEngine RenderingEngine { get; }

        /// <summary>
        /// Disposes the rendering manager and the rendering engine
        /// implementation used.
        /// </summary>
        public void Dispose()
        {
            RenderingEngine.Dispose();
        }
    }
}
