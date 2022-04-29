﻿using JSim.Core.Render;

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
            return 
                new OpenTKControl(
                    glContextFactory,
                    renderingEngine
                );
        }

        public void Destroy(OpenTKControl control)
        {
            control.Dispose();
        }
    }
}
