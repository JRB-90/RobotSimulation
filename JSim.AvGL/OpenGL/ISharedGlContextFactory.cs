using Avalonia.OpenGL;

namespace JSim.AvGL
{
    /// <summary>
    /// Interface for all shared context factories.
    /// Responsible for creating OpenGL contexts that are all shared with
    /// each other.
    /// </summary>
    public interface ISharedGlContextFactory
    {
        /// <summary>
        /// Creates a new OpenGL context that is shared with all contexts
        /// created by the same instance of this object.
        /// </summary>
        /// <returns>Shared OpenGL context.</returns>
        IGlContext CreateSharedContext();
    }
}
