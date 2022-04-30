using Avalonia.OpenGL;
using Avalonia.OpenTK;

namespace JSim.OpenTK
{
    /// <summary>
    /// Provides an OpenTK implementation of a OpenGL shared context factory.
    /// </summary>
    public class OpenTKSharedContextFactory : ISharedGlContextFactory, IDisposable
    {
        public OpenTKSharedContextFactory()
        {
            rootContext = AvaloniaOpenTKIntegration.CreateCompatibleContext(null);
        }

        /// <summary>
        /// Disposes this factory and the root context.
        /// </summary>
        public void Dispose()
        {
            rootContext.Dispose();
        }

        /// <summary>
        /// Creates a new OpenTK compatible OpenGL context that is shared with all
        /// contexts created by the same instance of this object.
        /// </summary>
        /// <returns>Shared OpenGL context.</returns>
        public IGlContext CreateSharedContext()
        {
            return AvaloniaOpenTKIntegration.CreateCompatibleContext(rootContext);
        }

        private IGlContext rootContext;
    }
}
