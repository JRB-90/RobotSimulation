using JSim.Core.Render;

namespace JSim.AvGL
{
    public class OpenGLControlFactory : IOpenGLControlFactory
    {
        readonly ISharedGlContextFactory glContextFactory;
        readonly IRenderingEngine renderingEngine;
        readonly ISurfaceManager surfaceManager;

        public OpenGLControlFactory(
            ISharedGlContextFactory glContextFactory,
            IRenderingEngine renderingEngine,
            ISurfaceManager surfaceManager)
        {
            this.glContextFactory = glContextFactory;
            this.renderingEngine = renderingEngine;
            this.surfaceManager = surfaceManager;
        }

        public void Dispose()
        {
        }

        public OpenGLControl CreateControl()
        {
            var surface =
                new OpenGLControl(
                    renderingEngine
                );

            surfaceManager.AddSurface(surface);

            return surface;
        }

        public void Destroy(OpenGLControl control)
        {
            surfaceManager.RemoveSurface(control);
            control.Dispose();
        }
    }
}
