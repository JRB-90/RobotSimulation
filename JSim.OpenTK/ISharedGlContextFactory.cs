using Avalonia.OpenGL;

namespace JSim.OpenTK
{
    public interface ISharedGlContextFactory
    {
        IGlContext CreateSharedContext();
    }
}
