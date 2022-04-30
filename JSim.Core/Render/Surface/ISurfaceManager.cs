namespace JSim.Core.Render
{
    /// <summary>
    /// Interface to define the behaviour of all rendering surface managers.
    /// Surface managers are responsible for managing all rendering surfaces
    /// used in a simulation application.
    /// </summary>
    public interface ISurfaceManager : IDisposable
    {
        /// <summary>
        /// Gets a collection of all rendering surfaces in the application.
        /// </summary>
        IReadOnlyCollection<IRenderingSurface> Surfaces { get; }

        /// <summary>
        /// Adds a surface to the collection of managed surfaces.
        /// </summary>
        /// <param name="surface">Surface to add.</param>
        /// <returns>True if add was successful.</returns>
        bool AddSurface(IRenderingSurface surface);

        /// <summary>
        /// Removes a surface from the collection of managed surfaces.
        /// </summary>
        /// <param name="surface">Surface to remove.</param>
        /// <returns>True if remove was successful.</returns>
        bool RemoveSurface(IRenderingSurface surface);
    }
}
