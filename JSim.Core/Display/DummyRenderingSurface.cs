﻿using JSim.Core.Render;

namespace JSim.Core.Display
{
    /// <summary>
    /// Dummy implementation of a rendering surface.
    /// </summary>
    public class DummyRenderingSurface : IRenderingSurface
    {
        public DummyRenderingSurface()
        {
            Camera = new StandardCamera(0, 0);
        }

        public int SurfaceWidth => 0;

        public int SurfaceHeight => 0;

        public ICamera Camera { get; set; }

        public event RenderRequestedEventHandler? RenderRequested;

        public void Dispose()
        {
        }
    }
}
