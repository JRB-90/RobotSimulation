using Avalonia.OpenGL;
using Avalonia.OpenTK;

namespace JSim.OpenTK
{
    public class OpenTKSharedContextFactory : ISharedGlContextFactory, IDisposable
    {
        public OpenTKSharedContextFactory()
        {
            rootContext = AvaloniaOpenTKIntegration.CreateCompatibleContext(null);
        }

        public void Dispose()
        {
            rootContext.Dispose();
        }

        public IGlContext CreateSharedContext()
        {
            return AvaloniaOpenTKIntegration.CreateCompatibleContext(rootContext);
        }

        private IGlContext rootContext;
    }
}
