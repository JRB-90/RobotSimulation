namespace JSim.Core.Render
{
    /// <summary>
    /// Interface to define the behaviour of all rendering managers.
    /// Rendering managers are responsible for tracking rendering configurations
    /// and controlling access to the implementation specific rendering code.
    /// </summary>
    public interface IRenderingManager : IDisposable
    {
        /// <summary>
        /// Rendering engine configured for this application.
        /// </summary>
        IRenderingEngine RenderingEngine { get; }
    }
}
