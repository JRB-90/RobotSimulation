namespace JSim.Core.Display
{
    /// <summary>
    /// Interface to define the behaviour of all display managers.
    /// Display managers are responsible for managing displays and rendering
    /// surfaces used in a simulation application.
    /// </summary>
    public interface IDisplayManager : IDisposable
    {
        /// <summary>
        /// Gets a collection of all rendering surfaces in the application.
        /// </summary>
        IReadOnlyCollection<IRenderingSurface> Surfaces { get; }

        /// <summary>
        /// Event fired when a managed surface requires rendering.
        /// </summary>
        event SurfaceRequiresRenderEventHandler? SurfaceRequiresRender;

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
