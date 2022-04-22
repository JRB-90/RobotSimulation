namespace JSim.Core.Display
{
    /// <summary>
    /// Defines a onscreen surface that can be renderered to.
    /// </summary>
    public interface IRenderingSurface
    {
        /// <summary>
        /// Gets the surfaces renderable width.
        /// </summary>
        int SurfaceWidth
        {
            get;
        }

        /// <summary>
        /// Gets the surfaces renderable height.
        /// </summary>
        int SurfaceHeight
        {
            get;
        }
    }
}
