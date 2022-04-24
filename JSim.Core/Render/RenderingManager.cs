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

        public IRenderingEngine RenderingEngine { get; }

        public void Dispose()
        {
            RenderingEngine.Dispose();
        }
    }
}
