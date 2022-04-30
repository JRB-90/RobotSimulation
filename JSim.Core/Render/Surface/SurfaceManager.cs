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

        /// <summary>
        /// Gets a collection of all rendering surfaces in the application.
        /// </summary>
        public IReadOnlyCollection<IRenderingSurface> Surfaces => surfaces;

        /// <summary>
        /// Disposes the surface manager and any managed surfaces.
        /// </summary>
        public void Dispose()
        {
            foreach (var surface in surfaces)
            {
                surface.Dispose();
            }
        }


        /// <summary>
        /// Adds a surface to the collection of managed surfaces.
        /// </summary>
        /// <param name="surface">Surface to add.</param>
        /// <returns>True if add was successful.</returns>
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

        /// <summary>
        /// Removes a surface from the collection of managed surfaces.
        /// </summary>
        /// <param name="surface">Surface to remove.</param>
        /// <returns>True if remove was successful.</returns>
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
