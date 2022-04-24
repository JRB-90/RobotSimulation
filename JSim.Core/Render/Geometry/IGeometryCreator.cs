namespace JSim.Core.Render
{
    /// <summary>
    /// Interface defining the behaviour of all scene object creators.
    /// </summary>
    public interface IGeometryCreator : IDisposable
    {
        /// <summary>
        /// Creates a new geometry object.
        /// </summary>
        /// <param name="parentGeometry">Parent node this geometry should be attached to.</param>
        /// <returns>New geometry object.</returns>
        IGeometry CreateGeometry(IGeometry? parentGeometry);
    }
}
