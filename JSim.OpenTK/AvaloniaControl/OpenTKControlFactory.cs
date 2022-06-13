using JSim.Core.Render;

namespace JSim.OpenTK
{
    public class OpenTKControlFactory : IOpenTKControlFactory
    {
        readonly ISharedGlContextFactory glContextFactory;
        readonly IRenderingEngine renderingEngine;

        public OpenTKControlFactory(
            ISharedGlContextFactory glContextFactory,
            IRenderingEngine renderingEngine)
        {
            this.glContextFactory = glContextFactory;
            this.renderingEngine = renderingEngine;
        }

        public void Dispose()
        {
        }

        public OpenTKControl CreateControl()
        {
            var control = 
                new OpenTKControl(
                    glContextFactory,
                    renderingEngine
                );

            // TODO - Add to display manager

            return control;
        }

        public void Destroy(OpenTKControl control)
        {
            control.Dispose();
        }
    }
}
