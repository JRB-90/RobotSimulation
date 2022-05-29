using JSim.Core.SceneGraph;

namespace JSim.Core.Render
{
    /// <summary>
    /// Dummy implementation of a rendering surface.
    /// </summary>
    public class DummyRenderingSurface : IRenderingSurface
    {
        public DummyRenderingSurface()
        {
            Camera = new StandardCamera(0, 0);
            SceneLighting = new SceneLighting();
        }

        public int SurfaceWidth => 0;

        public int SurfaceHeight => 0;

        public ICamera? Camera { get; set; }

        public IScene? Scene { get; set;}

        public SceneLighting SceneLighting { get; }

        public void Dispose()
        {
        }

        public void RequestRender()
        {
        }
    }
}
