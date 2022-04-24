namespace JSim.Core.Display
{
    /// <summary>
    /// Standard implementation of display manager.
    /// </summary>
    public class DisplayManager : IDisplayManager
    {
        public DisplayManager()
        {
            surfaces = new List<IRenderingSurface>();
        }

        public IReadOnlyCollection<IRenderingSurface> Surfaces =>
            surfaces;

        public event SurfaceRequiresRenderEventHandler? SurfaceRequiresRender;

        public void Dispose()
        {
            foreach (var surface in surfaces)
            {
                surface.Dispose();
            }
        }

        public bool AddSurface(IRenderingSurface surface)
        {
            if (surfaces.Contains(surface))
            {
                return false;
            }
            else
            {
                surfaces.Add(surface);
                surface.RenderRequested += OnSurfaceRequestedRender;
                SurfaceRequiresRender?.Invoke(this, new SurfaceRequiresRenderEventArgs(surface));

                return true;
            }
        }

        public bool RemoveSurface(IRenderingSurface surface)
        {
            if (!surfaces.Contains(surface))
            {
                return false;
            }
            else
            {
                surfaces.Remove(surface);
                surface.RenderRequested -= OnSurfaceRequestedRender;

                return true;
            }
        }

        private void OnSurfaceRequestedRender(object sender, Render.RenderRequestedEventArgs args)
        {
            if (sender is IRenderingSurface surface)
            {
                SurfaceRequiresRender?.Invoke(this, new SurfaceRequiresRenderEventArgs(surface));
            }
        }

        private List<IRenderingSurface> surfaces;
    }
}
