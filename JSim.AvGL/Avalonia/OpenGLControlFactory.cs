using JSim.Core.Render;

namespace JSim.AvGL
{
    public class OpenGLControlFactory : IOpenGLControlFactory
    {
        readonly ISharedGlContextFactory glContextFactory;
        readonly IRenderingEngine renderingEngine;

        public OpenGLControlFactory(
            ISharedGlContextFactory glContextFactory,
            IRenderingEngine renderingEngine)
        {
            this.glContextFactory = glContextFactory;
            this.renderingEngine = renderingEngine;
        }

        public void Dispose()
        {
        }

        public OpenGLControl CreateControl()
        {
            return
                new OpenGLControl(
                    renderingEngine
                );
        }

        public void Destroy(OpenGLControl control)
        {
            control.Dispose();
        }
    }
}
