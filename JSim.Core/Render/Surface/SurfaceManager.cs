namespace JSim.Core.Render
{
    /// <summary>
    /// Standard implementation of a rendering surface manager.
    /// </summary>
    public class SurfaceManager : ISurfaceManager
    {
        public SurfaceManager()
        {
            surfaces = new List<IRenderingSurface>();
        }

        public IReadOnlyCollection<IRenderingSurface> Surfaces =>
            surfaces;

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

                return true;
            }
        }

        private List<IRenderingSurface> surfaces;
    }
}
