using Avalonia;
using Avalonia.Logging;
using Avalonia.OpenGL;

namespace JSim.AvGL
{
    /// <summary>
    /// Provides a Avalonia based OpenGL shared context factory.
    /// </summary>
    public class SharedContextFactory : ISharedGlContextFactory, IDisposable
    {
        public void Dispose()
        {
        }

        /// <summary>
        /// Creates a new OpenTK compatible OpenGL context that is shared with all
        /// contexts created by the same instance of this object.
        /// </summary>
        /// <returns>Shared OpenGL context.</returns>
        public IGlContext CreateSharedContext()
        {
            var feature = AvaloniaLocator.Current.GetService<IPlatformOpenGlInterface>();

            if (feature == null)
            {
                throw new InvalidOperationException("Avalonia does not have the OpenGL platform interface");
            }

            if (!feature.CanShareContexts)
            {
                Logger.TryGet(LogEventLevel.Error, "OpenGL")?
                    .Log(
                        "OpenGlControlBase",
                        "Unable to initialize OpenGL: current platform does not support multithreaded context sharing"
                    );

                throw new InvalidOperationException("Unable to initialize OpenGL: current platform does not support multithreaded context sharing");
            }
            try
            {
                return feature.CreateSharedContext();
            }
            catch (Exception e)
            {
                Logger.TryGet(LogEventLevel.Error, "OpenGL")?
                    .Log(
                        "OpenGlControlBase",
                        "Unable to initialize OpenGL: unable to create additional OpenGL context: {exception}",
                        e
                    );

                throw new InvalidOperationException("Unable to initialize OpenGL: unable to create additional OpenGL context");
            }
        }
    }
}
